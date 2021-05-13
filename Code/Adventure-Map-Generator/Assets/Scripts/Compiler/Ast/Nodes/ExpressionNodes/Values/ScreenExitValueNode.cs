namespace Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values
{
    public sealed class ScreenExitValueNode : ExitValueNode
    {
        public string ConnectedScreenIdentifier { get; }
        
        public ScreenExitValueNode(string connectedScreenIdentifier)
        {
            ConnectedScreenIdentifier = connectedScreenIdentifier;
        }
    }
}