using System.Numerics;
using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ExitValueNode : ValueNode
    {
        public Vector2 Coordinates { get; }

        public ExitValueNode()
        {
            Type = SymbolType.Exit;
        }

        public ExitValueNode(Vector2 coordinates)
        {
            Coordinates = coordinates;
        }
        
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"[{Coordinates.X}, {Coordinates.Y}]";
        }
    }
}