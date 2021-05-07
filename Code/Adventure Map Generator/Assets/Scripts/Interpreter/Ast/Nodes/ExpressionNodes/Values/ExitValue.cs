using System.Numerics;
using P4.MapGenerator.Interpreter.Ast.Visitors;
using P4.MapGenerator.Interpreter.SemanticAnalysis;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values
{
    internal sealed class ExitValue : ValueNode
    {
        public Vector2 Coordinates { get; }

        public ExitValue()
        {
            this.Type = SymbolType.Exit;
        }

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