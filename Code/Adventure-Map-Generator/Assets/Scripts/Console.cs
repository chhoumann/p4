using System.Collections.Generic;
using Dazel.Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Dazel
{
    public sealed class Console : MonoBehaviour
    {
        [SerializeField] private GUISkin skin;

        private const float ButtonWidth = 100;
        private const float ButtonHeight = 20;
        
        private static float WindowWidth => Screen.width;
        private static float WindowHeight => Screen.height * 0.35f;
        
        private readonly List<LogMessage> logMessages = new List<LogMessage>();
       
        private Vector2 scrollPosition;

        private bool open;

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
            if (!GameManager.Instance.LoggingEnabled) return;
            
            open = type == LogType.Error || type == LogType.Warning;
            
            switch (type)
            {
                case LogType.Error:
                    logMessages.Add(new LogMessage(logString, Color.red));
                    break;
                case LogType.Warning:
                    logMessages.Add(new LogMessage(logString, Color.yellow));
                    break;
                case LogType.Log:
                    logMessages.Add(new LogMessage(logString, Color.white));
                    break;
            }
        }

        private void OnGUI()
        {
            if (!open || !GameManager.Instance.LoggingEnabled) return;
            
            GUI.skin = skin;
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true,
                GUILayout.Width(WindowWidth), GUILayout.Height(WindowHeight));

            foreach (LogMessage logMessage in logMessages)
            {
                GUI.contentColor = logMessage.MessageColor;
                GUILayout.Label(logMessage.Message);
            }
            
            GUILayout.EndScrollView();
            
            GUI.contentColor = Color.white;
            
            if (GUI.Button(new Rect(0, WindowHeight, ButtonWidth, ButtonHeight), "Close"))
            {
                open = false;
            }
            
            if (GUI.Button(new Rect(ButtonWidth, WindowHeight, ButtonWidth, ButtonHeight), "Clear"))
            {
                logMessages.Clear();
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