using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nazar.PubSubHub.Hub;

/// <summary>
///     The HandlerManager class is responsible for managing subscribers and their handlers in the PubSub pattern.
/// </summary>
public class HandlerManager
{
    /// <summary>
    ///     A list to store all handlers.
    /// </summary>
    public readonly List<Handler> Handlers = new();

    /// <summary>
    ///     A locker object used for synchronization purposes while accessing the handlers list.
    /// </summary>
    private readonly object _locker = new();

    /// <summary>
    ///     Adds a new handler for the specified subscriber and data type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is interested in.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">The delegate representing the handler.</param>
    public void AddHandler<T>(object subscriber, Delegate handler)
    {
        Handler item = new Handler
        {
            Action = handler,
            Sender = new WeakReference(subscriber),
            Type = typeof(T)
        };

        lock (_locker)
        {
            Handlers.Add(item);
        }
    }

    /// <summary>
    ///     Removes handlers for a specific subscriber with an optional handler filter.
    /// </summary>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">An optional delegate representing the handler to filter.</param>
    public void RemoveHandlers(object subscriber, Delegate handler = null)
    {
        lock (_locker)
        {
            var query = Handlers.Where(a => !a.Sender.IsAlive ||
                                             a.Sender.Target.Equals(subscriber));

            if (handler != null)
                query = query.Where(a => a.Action.Equals(handler));

            foreach (Handler h in query.ToList())
                Handlers.Remove(h);
        }
    }

    /// <summary>
    ///     Checks if there's a handler for a specific subscriber and data type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is interested in.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <returns>True if a handler exists, otherwise false.</returns>
    public void RemoveHandlers<T>(object subscriber, Delegate handler = null)
    {
        lock (_locker)
        {
            var query = Handlers.Where(a => !a.Sender.IsAlive ||
                                             a.Sender.Target.Equals(subscriber) && a.Type == typeof(T));

            if (handler != null)
                query = query.Where(a => a.Action.Equals(handler));

            foreach (Handler h in query.ToList())
                Handlers.Remove(h);
        }
    }


    /// <summary>
    ///     Checks if there's a specific handler for a specific subscriber and data type.
    /// </summary>
    /// <typeparam name="T">The type of data the handler is interested in.</typeparam>
    /// <param name="subscriber">The subscriber object.</param>
    /// <param name="handler">The action representing the handler.</param>
    /// <returns>True if the specific handler exists, otherwise false.</returns>
    public bool HandlerExists<T>(object subscriber)
    {
        lock (_locker)
        {
            return Handlers.Any(h => Equals(h.Sender.Target, subscriber) && typeof(T) == h.Type);
        }
    }

    /// <summary>
    ///     Executes all handlers for a given data type.
    /// </summary>
    /// <typeparam name="T">The type of data to pass to the handlers.</typeparam>
    /// <param name="data">The data to pass to the handlers.</param>
    public bool HandlerExists<T>(object subscriber, Action<T> handler)
    {
        lock (_locker)
        {
            return Handlers.Any(h =>
                Equals(h.Sender.Target, subscriber) && typeof(T) == h.Type && h.Action.Equals(handler));
        }
    }

    /// <summary>
    ///     Executes all handlers for a given data type.
    /// </summary>
    /// <typeparam name="T">The type of data to pass to the handlers.</typeparam>
    /// <param name="data">The data to pass to the handlers.</param>
    public void ExecuteHandlers<T>(T data)
    {
        var handlers = GetAliveHandlers<T>();
        foreach (Handler handler in handlers)
            switch (handler.Action)
            {
                case Action<T> action:
                    action(data);
                    break;
                case Func<T, Task> func:
                    func(data);
                    break;
            }
    }

    /// <summary>
    ///     Executes all handlers asynchronously for a given data type.
    /// </summary>
    /// <typeparam name="T">The type of data to pass to the handlers.</typeparam>
    /// <param name="data">The data to pass to the handlers.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ExecuteHandlersAsync<T>(T data)
    {
        var handlers = GetAliveHandlers<T>();
        foreach (Handler handler in handlers)
            switch (handler.Action)
            {
                case Action<T> action:
                    action(data);
                    break;
                case Func<T, Task> func:
                    await func(data);
                    break;
            }
    }

    /// <summary>
    ///     Returns a list of handlers that are still alive (not garbage collected) for a given data type.
    /// </summary>
    /// <typeparam name="T">The type of data the handlers are interested in.</typeparam>
    /// <returns>A list of alive handlers.</returns>
    private List<Handler> GetAliveHandlers<T>()
    {
        PruneHandlers();
        return Handlers.Where(h => h.Type.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())).ToList();
    }

    /// <summary>
    ///     Removes handlers whose subscribers have been garbage collected.
    /// </summary>
    private void PruneHandlers()
    {
        lock (_locker)
        {
            for (int i = Handlers.Count - 1; i >= 0; --i)
                if (!Handlers[i].Sender.IsAlive)
                    Handlers.RemoveAt(i);
        }
    }
}