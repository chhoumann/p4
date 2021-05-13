using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.Ast.Nodes.GameObjectNodes
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