using Antlr4.Runtime;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class TypeHandler
    {
        public SymbolType CurrentType { get; private set; } = SymbolType.Null;

        public void SetType(SymbolType value, IToken token)
        {
            if (CurrentType == SymbolType.Exit)
            {
                DazelLogger.EmitError("Exits cannot be used in expressions.", token);
            }
                
            if (CurrentType == SymbolType.Null || CurrentType == SymbolType.Integer && (value == SymbolType.Float || value == SymbolType.Integer))
            {
                CurrentType = value;
                return;
            }
                
            if (CurrentType == SymbolType.Float && value == SymbolType.Integer)
            {
                return;
            }
                
            DazelLogger.EmitError($"Type mismatch. {value} is not {CurrentType}", token);
        }
    }
}