using Winch.Core.API.Events.Addressables;

namespace Winch.Core.API.Events;

public delegate void DelegateDefinitions<T>(T instance);
public delegate void AddressablesLoadedEventHandler<T>(object sender, AddressablesLoadedEventArgs<T> e);
public delegate void ArbitraryEventHandler<T>(params object[] parameters);