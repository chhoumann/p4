using System;

namespace Interpreter.SemanticAnalysis
{
    public interface ISymbolTableEntry
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
    }
}