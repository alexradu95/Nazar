﻿using Framework.PubSubHub.Hub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nazar.PubSubHub.UnitTests;

[TestClass]
public class CoreHubTests
{
    private object _condemnedSubscriber;
    private MessagingHub _hub;
    private object _preservedSubscriber;
    private object _subscriber;

    [TestInitialize]
    public void Setup()
    {
        _hub = new MessagingHub();
        _subscriber = new object();
        _condemnedSubscriber = new object();
        _preservedSubscriber = new object();
    }

    [TestMethod]
    public void Publish_CallsAllRegisteredActions()
    {
        // arrange
        int callCount = 0;
        _hub.Subscribe(new object(), new Action<string>(a => callCount++));
        _hub.Subscribe(new object(), new Action<string>(a => callCount++));

        // act
        _hub.Publish(default(string));

        // assert
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public void Publish_SpecialEvent_CaughtByBase()
    {
        // arrange
        int callCount = 0;
        _hub.Subscribe<Event>(_subscriber, a => callCount++);
        _hub.Subscribe(_subscriber, new Action<Event>(a => callCount++));

        // act
        _hub.Publish(new SpecialEvent());

        // assert
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public void Publish_BaseEvent_NotCaughtBySpecial()
    {
        // arrange
        int callCount = 0;
        _hub.Subscribe(_subscriber, new Action<SpecialEvent>(a => callCount++));
        _hub.Subscribe(_subscriber, new Action<Event>(a => callCount++));

        // act
        _hub.Publish(new Event());

        // assert
        Assert.AreEqual(1, callCount);
    }


    [TestMethod]
    public void Publish_CleansUpBeforeSending()
    {
        // arrange
        object liveSubscriber = new();

        // act
        _hub.Subscribe(_condemnedSubscriber, new Action<string>(a => { }));
        _hub.Subscribe(liveSubscriber, new Action<string>(a => { }));

        _condemnedSubscriber = null;
        GC.Collect();

        _hub.Publish(default(string));

        // assert
        Assert.AreEqual(1, _hub.HandlerManager.Handlers.Count);
        GC.KeepAlive(liveSubscriber);
    }

    [TestMethod]
    public void Subscribe_AddsHandlerToList()
    {
        // arrange
        var action = new Action<string>(a => { });

        // act
        _hub.Subscribe(_subscriber, action);

        // assert
        Handler? h = _hub.HandlerManager.Handlers.First();
        Assert.AreEqual(_subscriber, h.Sender.Target);
        Assert.AreEqual(action, h.Action);
        Assert.AreEqual(action.Method.GetParameters().First().ParameterType, h.Type);
    }

    [TestMethod]
    public void Unsubscribe_RemovesAllHandlers_OfAnyType_ForSender()
    {
        // act
        _hub.Subscribe(_preservedSubscriber, new Action<string>(a => { }));
        _hub.Subscribe(_subscriber, new Action<string>(a => { }));
        _hub.Unsubscribe(_subscriber);

        // assert
        Assert.IsTrue(_hub.HandlerManager.Handlers.Any(a => a.Sender.Target == _preservedSubscriber));
        Assert.IsFalse(_hub.HandlerManager.Handlers.Any(a => a.Sender.Target == _subscriber));
    }

    [TestMethod]
    public void Unsubscribe_RemovesAllHandlers_OfSpecificType_ForSender()
    {
        // arrange
        _hub.Subscribe(_subscriber, new Action<string>(a => { }));
        _hub.Subscribe(_subscriber, new Action<string>(a => { }));
        _hub.Subscribe(_preservedSubscriber, new Action<string>(a => { }));

        // act
        _hub.Unsubscribe<string>(_subscriber);

        // assert
        Assert.IsFalse(_hub.HandlerManager.Handlers.Any(a => a.Sender.Target == _subscriber));
    }

    [TestMethod]
    public void Unsubscribe_RemovesSpecificHandler_ForSender()
    {
        var actionToDie = new Action<string>(a => { });
        _hub.Subscribe(_subscriber, actionToDie);
        _hub.Subscribe(_subscriber, new Action<string>(a => { }));
        _hub.Subscribe(_preservedSubscriber, new Action<string>(a => { }));

        // act
        _hub.Unsubscribe(_subscriber, actionToDie);

        // assert
        Assert.IsFalse(_hub.HandlerManager.Handlers.Any(a => a.Action.Equals(actionToDie)));
    }

    [TestMethod]
    public void Exists_EventDoesExist()
    {
        var action = new Action<string>(a => { });

        _hub.Subscribe(_subscriber, action);

        Assert.IsTrue(_hub.Exists(_subscriber, action));
    }


    [TestMethod]
    public void Unsubscribe_CleanUps()
    {
        // arrange
        var actionToDie = new Action<string>(a => { });
        _hub.Subscribe(_subscriber, actionToDie);
        _hub.Subscribe(_subscriber, new Action<string>(a => { }));
        _hub.Subscribe(_condemnedSubscriber, new Action<string>(a => { }));

        _condemnedSubscriber = null;

        GC.Collect();

        // act
        _hub.Unsubscribe<string>(_subscriber);

        // assert
        Assert.AreEqual(0, _hub.HandlerManager.Handlers.Count);
    }

    [TestMethod]
    public void PubSubUnsubDirectlyToHub()
    {
        // arrange
        int callCount = 0;
        var action = new Action<Event>(a => callCount++);
        MessagingHub myhub = new();

        // this lies and subscribes to the static hub instead.
        myhub.Subscribe(new Action<Event>(a => callCount++));
        myhub.Subscribe(new Action<SpecialEvent>(a => callCount++));
        myhub.Subscribe(action);

        // act
        myhub.Publish(new Event());
        myhub.Publish(new SpecialEvent());
        myhub.Publish<Event>();

        // assert
        Assert.AreEqual(7, callCount);

        // unsubscribe
        // this lies and unsubscribes from the static hub instead.
        myhub.Unsubscribe<SpecialEvent>();

        // act
        myhub.Publish(new SpecialEvent());

        // assert
        Assert.AreEqual(9, callCount);

        // unsubscribe specific action
        myhub.Unsubscribe(action);

        // act
        myhub.Publish(new SpecialEvent());

        // assert
        Assert.AreEqual(10, callCount);

        // unsubscribe to all
        myhub.Unsubscribe(myhub);

        // act
        myhub.Publish(new SpecialEvent());

        // assert
        Assert.AreEqual(10, callCount);
    }

    [TestMethod]
    public void Publish_NoExceptionRaisedWhenHandlerCreatesNewSubscriber()
    {
        // arrange
        _hub.Subscribe(new Action<Event>(a => new Stuff(_hub)));

        // act
        try
        {
            _hub.Publish(new Event());
        }

        // assert
        catch (InvalidOperationException e)
        {
            Assert.Fail($"Expected no exception, but got: {e}");
        }
    }

    internal class Stuff
    {
        public Stuff(MessagingHub hub)
        {
            hub.Subscribe(new Action<Event>(a => { }));
        }
    }
}

public class Event
{
}

public class SpecialEvent : Event
{
}