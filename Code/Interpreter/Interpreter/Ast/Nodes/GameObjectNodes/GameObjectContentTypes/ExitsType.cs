﻿namespace Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes
{
    public sealed class ExitsType : GameObjectContentType
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            throw new System.NotImplementedException();
        }
    }
}