using Nazar.Framework.Interfaces;
using StereoKit;
using StereoKit.Framework;

namespace Nazar.Framework;

public abstract class NazarApplication : INazarApplication
{
    private readonly Dictionary<Type, IAutonomousModule> _modules = new();

    public abstract bool RegisterPreInitModules();

    public abstract bool RegisterPostInitModules();

    public void Step()
    {
        foreach (var module in _modules.Values)
        {
           module.Step();
        }
    }


    public void Shutdown()
    {
        foreach (var module in _modules.Values)
        {
            module.Shutdown();
        }

        _modules.Clear();
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

    public void RegisterHandMenuFromModules()
    {
        var moduleHandLayers = _modules.Values
            .Where(autonomousModule => autonomousModule.ModuleHandMenuShortcuts != null)
            .Select(autonomousModule => autonomousModule.ModuleHandMenuShortcuts)
            .OfType<HandRadialLayer>()
            .ToArray();
        SK.AddStepper(new HandMenuRadial(moduleHandLayers));
    }

}
