using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.Ast.Visitors;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectContent : GameObjectNode
    {
        public GameObjectContentType Type { get; set; }
        public List<StatementNode> Statements { get; set; }

        public override void Accept(IGameObjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}