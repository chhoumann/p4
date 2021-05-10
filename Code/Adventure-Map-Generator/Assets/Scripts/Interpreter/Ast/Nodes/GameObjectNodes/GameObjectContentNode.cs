using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectContentNode : GameObjectNodeBase
    {
        public GameObjectContentTypeNode TypeNode { get; set; }
        public List<StatementNode> Statements { get; set; }

        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}