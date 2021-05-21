using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.StandardLibrary.Functions.MapFunctions
{
    public sealed class SizeFunction : Function
    {
        public override int NumArguments => 2;
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SizeFunction() : base(SymbolType.Void) { }

        public override ValueNode GetReturnType(List<ValueNode> parameters)
        {
            foreach (ValueNode valueNode in parameters)
            {
                if (valueNode.Type != SymbolType.Integer)
                {
                    DazelLogger.EmitError("Invalid type.", valueNode.Token);
                }
            }

            if (parameters[0] is IntValueNode width && parameters[1] is IntValueNode height)
            {
                Width = width.Value;
                Height = height.Value;
            }

            return null;
        }
    }
}