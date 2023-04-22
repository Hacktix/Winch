using UnityEngine;

namespace Winch.Core
{
    internal class AssetLoaderObject : MonoBehaviour
    {
        public void Start()
        {
            AssetLoader.LoadAssets();
        }
    }
}
