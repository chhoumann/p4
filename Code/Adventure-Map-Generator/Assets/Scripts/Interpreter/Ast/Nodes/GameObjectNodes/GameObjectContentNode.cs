using System.Collections.Generic;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Interpreter.Ast.Nodes.StatementNodes;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.Ast.Nodes.GameObjectNodes
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