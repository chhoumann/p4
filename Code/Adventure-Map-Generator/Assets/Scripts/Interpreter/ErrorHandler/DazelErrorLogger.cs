using System.Collections.Generic;
using UnityEngine;

namespace Dazel.Interpreter.ErrorHandler
{
    public sealed class DazelErrorLogger : IErrorLogger
    {
        private readonly List<string> errors = new List<string>();

        public void Log()
        {
            foreach (string error in errors)
            {
                Debug.LogError(error);
            }
        }

        public void AddToErrorList(string error)
        {
            errors.Add(error);
        }

        public bool HasErrors()
        {
            return errors.Count > 0;
        }
    }
}