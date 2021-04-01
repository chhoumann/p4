namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectType : GameObjectNode
    {
        public string Identifier { get; private set; }


        public GameObjectType(DazelParser.GameObjectTypeContext context)
        {
            Identifier = context.Parent.GetChild(1).GetText();
        }
    }
}