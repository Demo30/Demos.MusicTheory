#nullable enable

using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.Services;

internal class ServicesProvider
{
    internal readonly Dictionary<Type, Func<object>> Services = new();
    
    public void RegisterService<TService>(Func<TService> init)
    {
        var type = typeof(TService);
        if (Services.ContainsKey(type))
            return;
        
        Services.Add(type, () => init()!);
    }
}