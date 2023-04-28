using UnityEngine;
using Winch.Core.API;

namespace Winch.Core
{
    internal class AssetLoaderObject : MonoBehaviour
    {
        public void Start()
        {
            AssetLoader.LoadAssets();
            DredgeEvent.TriggerModAssetsLoaded();
            ModAssemblyLoader.ExecuteModAssemblies();
        }
    }
}
