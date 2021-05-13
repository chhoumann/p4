using System.Numerics;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.SemanticAnalysis;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public abstract class ExitValueNode : ValueNode
    {
        public override SymbolType Type => SymbolType.Exit;

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public sealed class TileExitValueNode : ExitValueNode
    {
        public Vector2 Coordinates { get; }
        
        public TileExitValueNode(Vector2 coordinates)
        {
            Coordinates = coordinates;
        }
        
        public override string ToString()
        {
            return $"[{Coordinates.X}, {Coordinates.Y}]";
        }
    }

    public sealed class ScreenExitValueNode : ExitValueNode
    {
        public string ConnectedScreenIdentifier { get; }
        
        public ScreenExitValueNode(string connectedScreenIdentifier)
        {
            ConnectedScreenIdentifier = connectedScreenIdentifier;
        }
    }
}