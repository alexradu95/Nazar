using System;

/// <summary>
/// Provides an interface for the subscriber in the PubSub pattern. The subscriber is responsible for listening to events published by the publisher.
/// </summary>
namespace Framework.PubSubHub.Interfaces;

public interface ISubscriber
{
    /// <summary>
    ///     Checks if a handler exists for the specified subscriber and type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is subscribed to.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <returns>True if a handler exists, otherwise false.</returns>
    bool Exists<T>(object subscriber);

    /// <summary>
    ///     Checks if a specific handler exists for the specified subscriber and type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is subscribed to.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">The specific handler to check for.</param>
    /// <returns>True if the handler exists, otherwise false.</returns>
    bool Exists<T>(object subscriber, Action<T> handler);

    /// <summary>
    ///     Subscribes the specified handler to the specified subscriber and type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler will be subscribed to.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">The handler to subscribe.</param>
    void Subscribe<T>(object subscriber, Action<T> handler);

    /// <summary>
    ///     Unsubscribes all handlers for the specified subscriber.
    /// </summary>
    /// <param name="subscriber">The subscriber object.</param>
    void Unsubscribe(object subscriber);

    /// <summary>
    ///     Unsubscribes all handlers for the specified subscriber and type.
    /// </summary>
    /// <typeparam name="T">The type of data the handlers are subscribed to.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    void Unsubscribe<T>(object subscriber);

    /// <summary>
    ///     Unsubscribes the specified handler for the specified subscriber and type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is subscribed to.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">The specific handler to unsubscribe.</param>
    void Unsubscribe<T>(object subscriber, Action<T> handler);
}