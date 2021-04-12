using System;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class MovePatternType : GameObjectType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}