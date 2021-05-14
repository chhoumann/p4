using System.Numerics;

namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class TileExitValueNode : ExitValueNode
    {
        public Vector2 Coordinates { get; }

        public MemberAccessNode ToExit { get; }

        public TileExitValueNode(Vector2 coordinates, MemberAccessNode toExit)
        {
            Coordinates = coordinates;
            ToExit = toExit;
        }
        
        public override string ToString()
        {
            return $"From [{Coordinates.X}, {Coordinates.Y}] to {string.Join(".", ToExit.Identifiers)}";
        }
    }
}