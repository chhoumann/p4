using System;

namespace Interpreter.Ast
{
    public class ScopeRow
    {
        public string Identifier { get; set; }
        public string Type { get; set; }
        public Boolean IsDeclaration { get; set; }
    }
}