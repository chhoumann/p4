using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace Dazel.Interpreter.Ast.Visitors
{
    public interface IExpressionVisitor
    {
        #region Expression
        void Visit(FactorExpressionNode factorExpressionNode);
        void Visit(FactorOperationNode factorOperationNode);
        void Visit(SumExpressionNode sumExpressionNode);
        void Visit(SumOperationNode sumOperationNode);
        void Visit(TerminalExpressionNode terminalExpressionNode);
        #endregion

        #region Values
        void Visit(MemberAccessNode memberAccessNode);
        void Visit(FloatValueNode floatValueNode);
        void Visit(IdentifierValueNode identifierValueNode);
        void Visit(IntValueNode intValueNode);
        void Visit(ArrayNode arrayNode);
        void Visit(StringNode stringNode);
        void Visit(ExitValueNode exitValueNode);
        #endregion
    }
}