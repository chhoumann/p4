using System;

namespace Interpreter.SemanticAnalysis
{
    public interface IScopeRow
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
    }
}