using UnityEngine;

namespace CardGameVR.UI
{
    public class LobbySetup : MonoBehaviour, ISubMenu
    {
        public void Show(bool active, string args)
        {
            gameObject.SetActive(active);
            
        }
    }
}