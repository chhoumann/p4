using Interpreter.Ast.Nodes.ExpressionNodes;

namespace Interpreter.Ast.Visitors
{
    public interface IExpressionVisitor
    {
        void Visit(FactorExpression factorExpression);
        void Visit(FactorOperation factorOperation);
        void Visit(SumExpression sumExpression);
        void Visit(SumOperation sumOperation);
        void Visit(TerminalExpression terminalExpression);
    }
}