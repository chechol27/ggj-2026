using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameServices
{
    public static Dictionary<Type, Component> services = new Dictionary<Type, Component>();

    public static TService Get<TService>() where TService : Component, IGameService
    {
        Type t = typeof(TService);
        if (!services.ContainsKey(t))
        {
            GameObject go = new GameObject($"{typeof(TService).Name}");
            services[t] = go.AddComponent<TService>();
        }

        return (TService)services[t];
    } 
}
