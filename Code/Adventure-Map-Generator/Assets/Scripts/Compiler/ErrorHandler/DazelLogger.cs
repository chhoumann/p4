using System;
using Antlr4.Runtime;
using UnityEngine;

namespace Dazel.Compiler.ErrorHandler
{
    public static class DazelLogger
    {
        public static event Action<string, LogType> LogMessageReceived;

        public static bool ThrowExceptions { get; set; }

        public static void EmitError(string message, IToken token)
        {
            string output = $"Error on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Error);

            if (ThrowExceptions)
            {
                throw new Exception(message);
            }
        }
        
        public static void EmitWarning(string message, IToken token)
        {
            string output = $"Warning on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Warning);
        }
        
        public static void EmitMessage(string message, IToken token)
        {
            string output = $"Message on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Log);
        }
    }
}