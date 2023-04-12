using System;

namespace PubSubHub.Hub;

/// <summary>
/// Represents a handler in the PubSub pattern, which contains information about the subscriber, 
/// the action to execute, and the type of data the handler is interested in.
/// </summary>

public class Handler
{
    /// <summary>
    /// Gets or sets the action to be executed when the handler is triggered. 
    /// This delegate can be either an action or a function returning a Task.
    /// </summary>
    public Delegate Action { get; set; }

    /// <summary>
    /// Gets or sets a weak reference to the subscriber that owns this handler. 
    /// Using a weak reference allows the subscriber to be garbage collected if no 
    /// other strong references to it exist, preventing memory leaks.
    /// </summary>
    public WeakReference Sender { get; set; }

    /// <summary>
    /// Gets or sets the type of data the handler is interested in. 
    /// When an event is published, only handlers with matching data types will be executed.
    /// </summary>
    public Type Type { get; set; }
}

