using System.Collections.Generic;
using UnityEngine;

namespace Dazel
{
    public sealed class ConsoleToGUI : MonoBehaviour
    {
        [SerializeField] private GUISkin skin;
        
        private Vector2 scrollPosition;

        private bool open;

        private readonly List<LogMessage> logMessages = new List<LogMessage>();

        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        private void Log(string logString, string stackTrace, LogType type)
        {
            open = type == LogType.Error || type == LogType.Warning;
            
            switch (type)
            {
                case LogType.Error:
                    logMessages.Add(new LogMessage(logString, Color.red));
                    break;
                case LogType.Warning:
                    logMessages.Add(new LogMessage(logString, Color.yellow));
                    break;
            }
        }

        private void OnGUI()
        {
            if (!open) return;
            
            GUI.skin = skin;
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, false,
                GUILayout.Width(Screen.width), GUILayout.Height(Screen.height * 0.5f));

            foreach (LogMessage logMessage in logMessages)
            {
                GUI.contentColor = logMessage.MessageColor;
                GUILayout.Label(logMessage.Message);
            }
            
            GUILayout.EndScrollView();
            
            GUI.contentColor = Color.white;
            
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f, 100, 20), "Close"))
            {
                open = false;
            }
        }

        private readonly struct LogMessage
        {
            public readonly string Message;
            public readonly Color MessageColor;

            public LogMessage(string message, Color messageColor)
            {
                Message = message;
                MessageColor = messageColor;
            }
        }
    }
}