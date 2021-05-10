using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace P4.MapGenerator.Interpreter.Ast.Visitors
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