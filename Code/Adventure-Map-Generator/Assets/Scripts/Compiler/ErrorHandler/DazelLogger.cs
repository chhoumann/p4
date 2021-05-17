using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dazel.Compiler.ErrorHandler
{
    public sealed class DazelLogger
    {
        public static event Action<string, string, LogType> LogMessageReceived; 

        public void EmitError(string error, string stackTrace)
        {
            LogMessageReceived?.Invoke(error, stackTrace, LogType.Error);
        }
        
        public void EmitWarning(string error, string stackTrace)
        {
            LogMessageReceived?.Invoke(error, stackTrace, LogType.Warning);
        }
        
        public void EmitMessage(string error, string stackTrace)
        {
            LogMessageReceived?.Invoke(error, stackTrace, LogType.Log);
        }
    }
}