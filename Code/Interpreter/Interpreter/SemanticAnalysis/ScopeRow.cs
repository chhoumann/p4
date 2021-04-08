using System;

namespace Interpreter.SemanticAnalysis
{
    public sealed class ScopeRow : IScopeRow
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
        public bool IsDeclaration { get; set; }

        public override string ToString()
        {
            return $"'{Identifier}': {nameof(Type)}: '{(Type == null ? "" : Type.Name)}'. {nameof(IsDeclaration)}: {IsDeclaration.ToString()}";
        }
    }
}