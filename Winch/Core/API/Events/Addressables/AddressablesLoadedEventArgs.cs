using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Winch.Core.API.Events.Addressables
{
    public struct AddressablesLoadedEventArgs<T>
    {
        public AsyncOperationHandle<IList<T>> Handle;

        public AddressablesLoadedEventArgs(AsyncOperationHandle<IList<T>> handle)
        {
            Handle = handle;
        }
    }
}
