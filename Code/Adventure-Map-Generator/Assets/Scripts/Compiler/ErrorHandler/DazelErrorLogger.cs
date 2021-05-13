using System.Collections.Generic;
using UnityEngine;

namespace Dazel.Compiler.ErrorHandler
{
    public sealed class DazelErrorLogger : IErrorLogger
    {
        public bool HasErrors => errors.Count > 0;
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
    }
}