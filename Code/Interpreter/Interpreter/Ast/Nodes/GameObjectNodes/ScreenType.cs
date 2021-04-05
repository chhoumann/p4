using System;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class ScreenType : GameObjectType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            Console.Write("Screen ");
        }
    }
}