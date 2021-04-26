using System.Numerics;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ExitValue : ValueNode
    {
        public Vector2 Coordinates { get; }

        public ExitValue(Vector2 coordinates)
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