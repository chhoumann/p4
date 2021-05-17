using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions
{
    public sealed class PrintFunction : Function
    {
        public PrintFunction() : base(SymbolType.Void) { }

        public override int NumArguments => 1;

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            ValueNode = parameters[0];
            return null;
        }

        public void Log()
        {
            DazelCompiler.Logger.EmitMessage(ValueNode.ToString(), ValueNode.Token);
        }
    }
}