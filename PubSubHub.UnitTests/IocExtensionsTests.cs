using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSubHub.PipelineFactory.PubSub;
using PubSubHub.Pub.Interfaces;
using PubSubHub.Sub.Interfaces;

namespace PubSub.Tests;

[TestClass]
public class IocExtensionsTests
{
    private object _preservedSender;
    private IPublisher _publisher;
    private IPubSubPipelineFactory _pubSubFactory;
    private object _sender;
    private ISubscriber _subscriber;

    [TestInitialize]
    public void Setup()
    {
        _pubSubFactory = new PubSubPipelineFactory();
        _subscriber = _pubSubFactory.GetSubscriber();
        _publisher = _pubSubFactory.GetPublisher();
        _sender = new object();
        _preservedSender = new object();
    }

    [TestMethod]
    public void Publish_Over_Interface_Calls_All_Subscribers()
    {
        int callCount = 0;
        _subscriber.Subscribe<Event>(_sender, a => callCount++);
        _subscriber.Subscribe(_sender, new Action<Event>(a => callCount++));

        _publisher.Publish(new SpecialEvent());

        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public void Unsubscribe_OverInterface_RemovesAllHandlers_OfAnyType_ForSender()
    {
        _subscriber.Subscribe(_preservedSender, new Action<Event>(a => { }));
        _subscriber.Subscribe(_sender, new Action<SpecialEvent>(a => { }));
        _subscriber.Unsubscribe(_sender);

        Assert.IsFalse(_subscriber.Exists<SpecialEvent>(_sender));
        Assert.IsTrue(_subscriber.Exists<Event>(_preservedSender));
    }

    [TestMethod]
    public void Unsubscribe_OverInterface_RemovesAllHandlers_OfSpecificType_ForSender()
    {
        _subscriber.Subscribe(_sender, new Action<string>(a => { }));
        _subscriber.Subscribe(_sender, new Action<string>(a => { }));
        _subscriber.Subscribe(_preservedSender, new Action<string>(a => { }));

        _subscriber.Unsubscribe<string>(_sender);

        Assert.IsFalse(_subscriber.Exists<string>(_sender));
    }

    [TestMethod]
    public void Unsubscribe_RemovesSpecificHandler_ForSender()
    {
        var actionToDie = new Action<string>(a => { });
        _subscriber.Subscribe(_sender, actionToDie);
        _subscriber.Subscribe(_sender, new Action<string>(a => { }));
        _subscriber.Subscribe(_preservedSender, new Action<string>(a => { }));

        _subscriber.Unsubscribe(_sender, actionToDie);

        Assert.IsFalse(_subscriber.Exists(_sender, actionToDie));
    }
}