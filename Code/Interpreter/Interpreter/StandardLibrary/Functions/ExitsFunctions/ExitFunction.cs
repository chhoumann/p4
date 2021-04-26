using System;
using System.Collections.Generic;
using System.Numerics;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.SemanticAnalysis;

namespace Interpreter.StandardLibrary.Functions.ExitsFunctions
{
    public sealed class ExitFunction : Function
    {
        public ExitFunction() : base(SymbolType.Void) { }

        private protected override Action call { get; }
        public override ValueNode Execute(List<ValueNode> parameters)
        {
            // call();

            try
            {
                if (parameters[0] is ArrayNode coords && parameters[1] is MemberAccess memberAccess)
                {
                    IntValue x = coords.Values[0] as IntValue;
                    IntValue y = coords.Values[1] as IntValue;

                    Vector2 vectorCoords = new(x.Value, y.Value);
                    

                    return new ExitValue(vectorCoords);
                }
                
                // TODO: Should return some exit type so we can assign to exits
                throw new ArgumentException("Invalid arguments passed to Exit function.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException("Invalid number of arguments");
            }
        }
    }
}