using Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace Interpreter.Ast.Visitors
{
    public interface IExpressionVisitor
    {
        #region Expression
        void Visit(FactorExpression factorExpression);
        void Visit(FactorOperation factorOperation);
        void Visit(SumExpression sumExpression);
        void Visit(SumOperation sumOperation);
        void Visit(TerminalExpression terminalExpression);
        #endregion

        #region Values
        void Visit(MemberAccess memberAccess);
        void Visit(FloatValue floatValue);
        void Visit(IdentifierValue identifierValue);
        void Visit(IntValue intValue);
        void Visit(ArrayNode arrayNode);
        #endregion

        void Visit(ExitValue exitValue);
    }
}