using System;
using System.Collections.Generic;
using System.Data;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class GameObject : GameObjectNode
    {
        public string Identifier { get; set; }
        public GameObjectType Type { get; set; }
        public List<GameObjectContent> Contents { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Type.PrintMe();
            Console.WriteLine(Identifier);
            foreach (GameObjectContent gameObjectContent in Contents)
            {
                gameObjectContent.PrintMe();
            }
        }
    }
}