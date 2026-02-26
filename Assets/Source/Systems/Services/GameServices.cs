using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class GameServices
{
    private static readonly Dictionary<Type, Component> services = new Dictionary<Type, Component>();

    public static TService Get<TService>() where TService : Component, IGameService
    {
        Type t = typeof(TService);
        if (!services.ContainsKey(t))
        {
            GameObject go = new ($"{typeof(TService).Name}");
            services[t] = go.AddComponent<TService>();
            Object.DontDestroyOnLoad(go);
        }

        return (TService)services[t];
    }
}
