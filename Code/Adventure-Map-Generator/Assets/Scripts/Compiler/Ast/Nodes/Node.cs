﻿namespace Dazel.Compiler.Ast.Nodes
{
    public abstract class Node<TVisitor>
    {
        public abstract void Accept(TVisitor visitor);
    }
}