using System;
using CNC.Interfaces.Events;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

//Add to prefab RootScope by Reflex Settings
public class RootInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.RegisterType(typeof(EventBus), new Type[] {typeof(IEventBus)}, Lifetime.Singleton, Resolution.Lazy);
    }
}