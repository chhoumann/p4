using System.Numerics;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
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
}