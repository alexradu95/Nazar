using Nazar.Framework.Interfaces;
using StereoKit;
using StereoKit.Framework;
using System.Reflection;

namespace Nazar.Framework
{
    public abstract class NazarApplication : INazarApplication
    {
        private Dictionary<Type, IAutonomousModule> _modules;

        private HandMenuRadial handMenu;

        public NazarApplication()
        {
            _modules = new Dictionary<Type, IAutonomousModule>();
        }

        public void GenerateHandMenu() => SK.AddStepper(new HandMenuRadial(GenerateModuleLayer()));

        private HandRadialLayer GenerateModuleLayer()
        {
            var layerItems = _modules.Values.Select(module => new HandMenuItem(module.Name, null, () => module.IsEnabled = !module.IsEnabled))
            .Concat(new[] { new HandMenuItem("Back", null, null, HandMenuAction.Back) })
            .ToArray();

            return new HandRadialLayer("Modules", layerItems);
        }

        public void RegisterModule<T>() where T : IAutonomousModule, new()
        {
            var moduleType = typeof(T);

            if (_modules.ContainsKey(moduleType))
            {
                throw new InvalidOperationException($"A module of type '{moduleType}' has already been registered.");
            }

            _modules.Add(moduleType, new T());

        }

        public void UnregisterModule<T>() where T : IAutonomousModule, new()
        {
            _modules.Remove(typeof(T));
        }

        public void EnableModule<T>() where T : IAutonomousModule, new()
        {
            var moduleType = typeof(T);

            if (_modules.TryGetValue(moduleType, out var module))
            {
                module.IsEnabled = true;
            }
        }

        public void DisableModule<T>() where T : IAutonomousModule, new()
        {
            var moduleType = typeof(T);

            if (_modules.TryGetValue(moduleType, out var module))
            {
                module.IsEnabled = false;
            }
        }

        public void Step()
        {
            foreach (var module in _modules.Values)
            {
                if (module.IsEnabled && module is IAutonomousModule stepperModule)
                {
                    stepperModule.Step();
                }
            }
        }

        public abstract bool RegisterPreInitModules();

        public abstract bool RegisterPostInitModules();

        public void Shutdown()
        {
            foreach (var module in _modules.Values)
            {
                if (module.IsEnabled && module is IAutonomousStepperModule stepperModule)
                {
                    stepperModule.Shutdown();
                }
            }
        }
    }
}
