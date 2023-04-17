using UnityEngine;
using Winch.Core;

namespace DisasterButton
{
    class DisasterButton : MonoBehaviour
    {
        private static System.Random rnd = new System.Random();

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Delete))
                OnPress();
        }

        private void OnPress()
        {
            if (GameManager.Instance == null || GameManager.Instance.DataLoader == null || GameManager.Instance.WorldEventManager == null)
                return;

            WinchCore.Log.Debug("DisasterButton initialized.");
            int index = rnd.Next(GameManager.Instance.DataLoader.allWorldEvents.Count);
            WorldEventData worldEvent = GameManager.Instance.DataLoader.allWorldEvents[index];
            WinchCore.Log.Debug($"Spawning event No. {index}: {worldEvent.name}");
            GameManager.Instance.WorldEventManager.DoEvent(worldEvent);
        }
    }
}
