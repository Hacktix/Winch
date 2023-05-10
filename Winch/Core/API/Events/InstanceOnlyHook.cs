using System;
using System.Linq;
using UnityEngine.EventSystems;

namespace Winch.Core.API.Events;

public class InstanceOnlyHook<T>
{
    public event DelegateDefinitions<T> Before;
    public event DelegateDefinitions<T> On;

    public void Trigger(T instance, bool prefix)
    {

        WinchCore.Log.Debug($"Triggered instance {typeof(T)} event : (Prefix: {prefix.ToString()})");
        try
        {
            if (prefix)
                Before?.Invoke(instance);
            else
                On?.Invoke(instance);
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Failed to trigger instance {typeof(T)} types event: {ex}");
        }

    }
}