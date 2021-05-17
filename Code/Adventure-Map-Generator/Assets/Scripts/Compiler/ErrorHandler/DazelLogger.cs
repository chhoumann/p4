using System;
using UnityEngine;

namespace Dazel.Compiler.ErrorHandler
{
    public sealed class DazelLogger
    {
        public static event Action<string, LogType> LogMessageReceived; 

        public void EmitError(string error)
        {
            LogMessageReceived?.Invoke(error, LogType.Error);
        }
        
        public void EmitWarning(string error)
        {
            LogMessageReceived?.Invoke(error, LogType.Warning);
        }
        
        public void EmitMessage(string error)
        {
            LogMessageReceived?.Invoke(error, LogType.Log);
        }
    }
}