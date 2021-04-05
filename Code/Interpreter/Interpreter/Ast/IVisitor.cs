using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public interface IVisitor
    {
        void Visit(Array array);
        void Visit(FactorExpression factorExpression);
        void Visit(FactorOperation factorOperation);
        void Visit(FloatValue floatValue);
        void Visit(IdentifierValue identifierValue);
        void Visit(IntValue intValue);
        void Visit(MemberAccess memberAccess);
        void Visit(SumExpression sumExpression);
        void Visit(SumOperation sumOperation);
        void Visit(TerminalExpression terminalExpression);
        void Visit(EntityType entityType);
        void Visit(GameObject gameObject);
        void Visit(GameObjectContent gameObjectContent);
        void Visit(MovePatternType movePatternType);
        void Visit(ScreenType gameObjectContent);
        void Visit(IfStatement ifStatement);
        void Visit(RepeatNode repeatNode);
        void Visit(StatementExpression statementExpression);
        void Visit(DataType dataType);
        void Visit(EntitiesType entitiesType);
        void Visit(ExitsType exitsType);
        void Visit(MapType mapType);
        void Visit(OnScreenEnteredType onScreenEnteredType);
        void Visit(PatternType patternType);
    }
}