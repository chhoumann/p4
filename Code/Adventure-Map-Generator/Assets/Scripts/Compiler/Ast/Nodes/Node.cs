using Antlr4.Runtime;

namespace Dazel.Compiler.Ast.Nodes
{
    public abstract class Node<TVisitor>
    {
        public ParserRuleContext Context { get; set; }
        
        public abstract void Accept(TVisitor visitor);
    }
}