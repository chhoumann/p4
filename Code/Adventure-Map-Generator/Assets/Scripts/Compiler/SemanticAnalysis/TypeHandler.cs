using Antlr4.Runtime;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class TypeHandler
    {
        public SymbolType CurrentType { get; private set; } = SymbolType.Null;

        public void SetType(SymbolType value, IToken token)
        {
            if (CurrentType == SymbolType.Exit)
            {
                DazelCompiler.Logger.EmitError("Exits cannot be used in expressions.", token);
            }
                
            if (CurrentType == SymbolType.Null || CurrentType == SymbolType.Integer && (value == SymbolType.Float || value == SymbolType.Integer))
            {
                CurrentType = value;
                return;
            }
                
            DazelCompiler.Logger.EmitError($"Type mismatch. {value} is not {CurrentType}", token);
        }
    }
}