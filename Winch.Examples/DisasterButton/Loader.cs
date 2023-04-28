using UnityEngine;

namespace DisasterButton
{
    public class Loader
    {
        public static void Initialize()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<DisasterButton>();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
