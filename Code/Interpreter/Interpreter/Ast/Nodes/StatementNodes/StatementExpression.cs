using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public class StatementExpression : StatementNode
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void PrintMe()
        {
            //Expression.PrintMe();
        }
    }
}