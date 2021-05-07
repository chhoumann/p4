namespace P4.MapGenerator.Interpreter.SemanticAnalysis
{
    internal abstract class SymbolTableEntry
    {
        public SymbolType Type { get; }

        protected SymbolTableEntry(SymbolType type)
        {
            Type = type;
        }
    }
}