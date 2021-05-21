using UnityEngine;
using UnityEngine.UI;

namespace Dazel.Game.Core
{
    public sealed class ToggleLogging : MonoBehaviour
    {
        private const string PlayerPrefsKey = "LoggingEnabled";
        
        private void Awake()
        {
            Toggle toggle = GetComponent<Toggle>();
            
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                bool isOn = PlayerPrefs.GetInt(PlayerPrefsKey) == 1;
                OnToggleChanged(isOn);
                toggle.SetIsOnWithoutNotify(isOn);
            }
            else
            {
                toggle.SetIsOnWithoutNotify(GameManager.Instance.LoggingEnabled);
            }
            
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private static void OnToggleChanged(bool isOn)
        {
            GameManager.Instance.LoggingEnabled = isOn;
            PlayerPrefs.SetInt(PlayerPrefsKey, isOn ? 1 : 0);
        }
    }
}
