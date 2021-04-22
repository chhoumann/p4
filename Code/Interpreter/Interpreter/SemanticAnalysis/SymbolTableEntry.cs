﻿namespace Interpreter.SemanticAnalysis
{
    public abstract class SymbolTableEntry
    {
        public SymbolType Type { get; }

        protected SymbolTableEntry(SymbolType type)
        {
            Type = type;
        }
    }
}