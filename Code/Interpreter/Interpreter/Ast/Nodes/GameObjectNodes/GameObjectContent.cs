using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObjectContent : GameObjectNode
    {
        public GameObjectContentType Type { get; set; }
        public List<StatementNode> Statements { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Console.WriteLine(Type.ToString());
            foreach (var statementNode in Statements)
            {
                statementNode.PrintMe();
            }
        }
    }
}