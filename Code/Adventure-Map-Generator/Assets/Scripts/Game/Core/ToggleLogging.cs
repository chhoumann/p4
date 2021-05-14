using UnityEngine;
using UnityEngine.UI;

namespace Dazel.Game.Core
{
    public sealed class ToggleLogging : MonoBehaviour
    {
        private void Awake()
        {
            Toggle toggle = GetComponent<Toggle>();
            toggle.SetIsOnWithoutNotify(GameManager.Instance.LoggingEnabled);
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private static void OnToggleChanged(bool isOn)
        {
            GameManager.Instance.LoggingEnabled = isOn;
        }
    }
}
