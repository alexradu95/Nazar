namespace Nazar.PubSubHub.Interfaces;

/// <summary>
///     Provides an interface for the publisher in the PubSub pattern. The publisher is responsible for sending events to
///     all subscribers.
/// </summary>
public interface IPublisher
{
    /// <summary>
    ///     Publishes an event of the specified data type to all subscribers that are listening for this event.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="data">The data associated with the event.</param>
    void Publish<T>(T data);
}