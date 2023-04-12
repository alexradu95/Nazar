﻿using System;
using PubSubHub.Hub;
using PubSubHub.Sub.Interfaces;

namespace PubSubHub.Sub
{
    /// <summary>
    /// The Subscriber class is an implementation of the ISubscriber interface, which provides functionality for listening to events published by the publisher in the PubSub pattern.
    /// </summary>
    public class Subscriber : ISubscriber
    {
        private readonly MessagingHub hub;

        /// <summary>
        /// Initializes a new instance of the Subscriber class.
        /// </summary>
        /// <param name="hub">The hub instance for managing handlers.</param>
        public Subscriber(MessagingHub hub)
        {
            this.hub = hub;
        }

        /// <summary>
        /// Checks if a handler exists for the specified subscriber and data type.
        /// </summary>
        /// <typeparam name="T">The type of data for the event.</typeparam>
        /// <param name="subscriber">The subscriber object.</param>
        /// <returns>Returns true if the handler exists, false otherwise.</returns>
        public bool Exists<T>(object subscriber) => hub.Exists<T>(subscriber);

        /// <summary>
        /// Checks if a specific handler exists for the specified subscriber and data type.
        /// </summary>
        /// <typeparam name="T">The type of data for the event.</typeparam>
        /// <param name="subscriber">The subscriber object.</param>
        /// <param name="handler">The handler to check for.</param>
        /// <returns>Returns true if the handler exists, false otherwise.</returns>
        public bool Exists<T>(object subscriber, Action<T> handler) => hub.Exists(subscriber, handler);

        /// <summary>
        /// Subscribes a handler for a specified data type.
        /// </summary>
        /// <typeparam name="T">The type of data for the event.</typeparam>
        /// <param name="subscriber">The subscriber object.</param>
        /// <param name="handler">The handler to subscribe.</param>
        public void Subscribe<T>(object subscriber, Action<T> handler) => hub.Subscribe(subscriber, handler);

        /// <summary>
        /// Unsubscribes all handlers for a specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber object.</param>
        public void Unsubscribe(object subscriber) => hub.Unsubscribe(subscriber);

        /// <summary>
        /// Unsubscribes all handlers for a specified subscriber and data type.
        /// </summary>
        /// <typeparam name="T">The type of data for the event.</typeparam>
        /// <param name="subscriber">The subscriber object.</param>
        public void Unsubscribe<T>(object subscriber) => hub.Unsubscribe<T>(subscriber);

        /// <summary>
        /// Unsubscribes a specific handler for a specified subscriber and data type.
        /// </summary>
        /// <typeparam name="T">The type of data for the event.</typeparam>
        /// <param name="subscriber">The subscriber object.</param>
        /// <param name="handler">The handler to unsubscribe.</param>
        public void Unsubscribe<T>(object subscriber, Action<T> handler) => hub.Unsubscribe(subscriber, handler);
    }
}