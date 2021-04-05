using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Nodes.StatementNodes
{
    public class StatementExpression : StatementNode
    {
        public ExpressionNode Expression { get; set; }

        public override void PrintMe()
        {
            //Expression.PrintMe();
            base.PrintMe();
        }
    }
}