namespace Nazar.Framework.Interfaces
{
    /// <summary>
    /// Handles the management of Nazar modules within the application.
    /// </summary>
    internal interface INazarApplication
    {
        /// <summary>
        /// Performs a step in the application, updating all registered modules.
        /// This is called once per frame.
        /// </summary>
        public void Step();

        /// <summary>
        /// Initializes the application and its registered modules.
        /// Called before SK.Initialize.
        /// </summary>
        public bool RegisterPreInitModules();

        /// <summary>
        /// Initializes the application and its registered modules.
        /// Called after SK.Initialize.
        /// </summary>
        public bool RegisterPostInitModules();

        /// <summary>
        /// Shuts down the application and its registered modules, performing any necessary cleanup.
        /// </summary>
        public void Shutdown();

        /// <summary>
        /// Registers a module of type T to the application. The module must have a unique name and type.
        /// </summary>
        /// <typeparam name="T">The type of the module to register, which must implement IAutonomousModule.</typeparam>
        protected internal void RegisterModule<T>() where T : IAutonomousModule, new();

        /// <summary>
        /// Unregisters a module of type T from the application.
        /// </summary>
        /// <typeparam name="T">The type of the module to unregister, which must implement IAutonomousModule.</typeparam>
        protected internal void UnregisterModule<T>() where T : IAutonomousModule, new();

        /// <summary>
        /// Enables a module of type T within the application.
        /// </summary>
        /// <typeparam name="T">The type of the module to enable, which must implement IAutonomousModule.</typeparam>
        protected internal void EnableModule<T>() where T : IAutonomousModule, new();

        /// <summary>
        /// Disables a module of type T within the application.
        /// </summary>
        /// <typeparam name="T">The type of the module to disable, which must implement IAutonomousModule.</typeparam>
        protected internal void DisableModule<T>() where T : IAutonomousModule, new();
    }
}
