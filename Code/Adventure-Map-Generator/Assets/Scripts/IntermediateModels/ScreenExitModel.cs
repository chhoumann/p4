namespace Dazel.IntermediateModels
{
    public sealed class ScreenExitModel
    {
        public string ConnectedScreenIdentifier { get; }
        public Direction ExitDirection { get; }

        public ScreenExitModel(string connectedScreenIdentifier, Direction exitDirection)
        {
            ConnectedScreenIdentifier = connectedScreenIdentifier;
            ExitDirection = exitDirection;
        }
    }
}