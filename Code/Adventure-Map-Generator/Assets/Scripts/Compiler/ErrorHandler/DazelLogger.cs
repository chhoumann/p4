﻿using System;
using Antlr4.Runtime;
using UnityEngine;

namespace Dazel.Compiler.ErrorHandler
{
    public sealed class DazelLogger
    {
        public static event Action<string, LogType> LogMessageReceived;

        public bool ThrowExceptions { get; set; }

        public DazelLogger(bool throwExceptions)
        {
            ThrowExceptions = throwExceptions;
        }
        
        public void EmitError(string message, IToken token)
        {
            string output = $"Error on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Error);

            if (ThrowExceptions)
            {
                throw new Exception(message);
            }
        }
        
        public void EmitWarning(string message, IToken token)
        {
            string output = $"Warning on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Warning);
        }
        
        public void EmitMessage(string message, IToken token)
        {
            string output = $"Message on line {token.Line} in {token.InputStream.SourceName}:\n{message}";
            LogMessageReceived?.Invoke(output, LogType.Log);
        }
    }
}