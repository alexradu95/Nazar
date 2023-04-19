namespace Nazar.PubSubHub.Hub;

/// <summary>
///     Represents the main class for the PubSub pattern, providing methods for publishing and subscribing
///     to events. This class uses a singleton pattern for a default instance, but can also be instantiated directly.
/// </summary>
public class MessagingHub
{
    private static MessagingHub _default;
    public readonly HandlerManager HandlerManager = new();

    /// <summary>
    ///     Gets the default Hub instance, which is created lazily on first access.
    /// </summary>
    public static MessagingHub Default => _default ??= new MessagingHub();

    /// <summary>
    ///     Publishes an event of the specified data type, executing all subscribed handlers synchronously.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="data">The data associated with the event. Defaults to the default value of the data type.</param>
    public void Publish<T>(T data = default)
    {
        HandlerManager.ExecuteHandlers(data);
    }

    /// <summary>
    ///     Publishes an event of the specified data type, executing all subscribed handlers asynchronously.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="data">The data associated with the event. Defaults to the default value of the data type.</param>
    public async Task PublishAsync<T>(T data = default)
    {
        await HandlerManager.ExecuteHandlersAsync(data);
    }

    /// <summary>
    ///     Subscribes the current instance to an event of the specified data type, providing an action to be executed when the
    ///     event is published.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="handler">The action to be executed when the event is published.</param>
    public void Subscribe<T>(Action<T> handler)
    {
        Subscribe(this, handler);
    }

    /// <summary>
    ///     Subscribes the current instance to an event of the specified data type, providing an asynchronous function to be
    ///     executed when the event is published.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="handler">The asynchronous function to be executed when the event is published.</param>
    public void Subscribe<T>(Func<T, Task> handler)
    {
        Subscribe(this, handler);
    }

    /// <summary>
    ///     Subscribes a specified subscriber to an event of the specified data type, providing an action to be executed when
    ///     the event is published.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="subscriber">The subscriber to be notified of the event.</param>
    /// <param name="handler">The action to be executed when the event is published.</param>
    public void Subscribe<T>(object subscriber, Action<T> handler)
    {
        HandlerManager.AddHandler<T>(subscriber, handler);
    }

    /// <summary>
    ///     Subscribes a specified subscriber to an event of the specified data type, providing an asynchronous function to be
    ///     executed when the event is published.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="subscriber">The subscriber to be notified of the event.</param>
    /// <param name="handler">The asynchronous function to be executed when the event is published.</param>
    public void Subscribe<T>(object subscriber, Func<T, Task> handler)
    {
        HandlerManager.AddHandler<T>(subscriber, handler);
    }

    /// <summary>
    ///     Unsubscribes the current instance from a specific event handler.
    /// </summary>
    /// <param name="handler">The specific event handler to unsubscribe from.</param>
    public void Unsubscribe(Delegate handler)
    {
        Unsubscribe(this, handler);
    }

    /// <summary>
    ///     Unsubscribes a specified subscriber from all events, with an optional handler filter.
    /// </summary>
    /// <param name="subscriber">The subscriber to be unsubscribed from events.</param>
    /// <param name="handler">An optional handler filter to limit the unsubscription.</param>
    public void Unsubscribe(object subscriber, Delegate handler = null)
    {
        HandlerManager.RemoveHandlers(subscriber, handler);
    }

    /// <summary>
    ///     Unsubscribes the current instance from all events of the specified data type.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    public void Unsubscribe<T>()
    {
        Unsubscribe<T>(this);
    }

    /// <summary>
    ///     Unsubscribes the current instance from a specific event handler of the specified data type.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="handler">The specific event handler to unsubscribe from.</param>
    public void Unsubscribe<T>(Delegate handler)
    {
        Unsubscribe(this, handler);
    }

    /// <summary>
    ///     Unsubscribes a specified subscriber from all events of the specified data type, with an optional handler filter.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="subscriber">The subscriber to be unsubscribed from events of the specified data type.</param>
    /// <param name="handler">An optional handler filter to limit the unsubscription.</param>
    public void Unsubscribe<T>(object subscriber, Delegate handler = null)
    {
        HandlerManager.RemoveHandlers<T>(subscriber, handler);
    }

    /// <summary>
    ///     Checks if a handler exists for the specified data type on the current instance.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <returns>true if a handler exists; otherwise, false.</returns>
    public bool Exists<T>()
    {
        return Exists<T>(this);
    }

    /// <summary>
    ///     Checks if a handler exists for the specified data type on a specified subscriber.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="subscriber">The subscriber to check for event handlers.</param>
    /// <returns>true if a handler exists; otherwise, false.</returns>
    public bool Exists<T>(object subscriber)
    {
        return HandlerManager.HandlerExists<T>(subscriber);
    }


    /// <summary>
    ///     Checks if a specific handler exists for the specified data type on a specified subscriber.
    /// </summary>
    /// <typeparam name="T">The data type of the event.</typeparam>
    /// <param name="subscriber">The subscriber to check for the specific event handler.</param>
    /// <param name="handler">The specific event handler to check for.</param>
    /// <returns>true if the specific handler exists; otherwise, false.</returns>
    public bool Exists<T>(object subscriber, Action<T> handler)
    {
        return HandlerManager.HandlerExists(subscriber, handler);
    }
}