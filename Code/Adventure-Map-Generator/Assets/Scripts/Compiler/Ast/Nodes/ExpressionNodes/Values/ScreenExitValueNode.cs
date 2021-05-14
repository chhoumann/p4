namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ScreenExitValueNode : ExitValueNode
    {
        public string ConnectedScreenIdentifier { get; }
        public Direction ExitDirection { get; }

        public ScreenExitValueNode(string connectedScreenIdentifier, Direction exitDirection)
        {
            ConnectedScreenIdentifier = connectedScreenIdentifier;
            ExitDirection = exitDirection;
        }
    }
}