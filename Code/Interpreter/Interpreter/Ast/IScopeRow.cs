using System;

namespace Interpreter.Ast
{
    public interface IScopeRow
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
    }
}