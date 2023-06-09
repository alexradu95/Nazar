﻿using Nazar.PubSubHub.Hub;
using Nazar.PubSubHub.Interfaces;

namespace Nazar.PubSubHub;

/// <summary>
///     The Publisher class is an implementation of the IPublisher interface, which provides functionality for sending
///     events to all subscribers in the PubSub pattern.
/// </summary>
public class Publisher : IPublisher
{
    private readonly MessagingHub _hub;

    /// <summary>
    ///     Initializes a new instance of the Publisher class with the specified Hub instance.
    /// </summary>
    /// <param name="hub">The Hub instance used for managing subscribers and handlers.</param>
    public Publisher(MessagingHub hub)
    {
        _hub = hub;
    }

    /// <summary>
    ///     Publishes an event of the specified data type to all subscribers that are listening for this event.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="data">The data associated with the event.</param>
    public void Publish<T>(T data)
    {
        _hub.Publish(data);
    }
}