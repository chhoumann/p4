namespace Interpreter.SemanticAnalysis
{
    public abstract class SymbolTableEntry
    {
        public string Identifier { get; }

        public SymbolType Type { get; }

        protected SymbolTableEntry(string identifier, SymbolType type)
        {
            Identifier = identifier;
            Type = type;
        }
    }
}