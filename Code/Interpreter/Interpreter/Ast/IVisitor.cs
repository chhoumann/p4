﻿using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public interface IVisitor
    {
        void Visit(GameObject gameObject);
        void Visit(MapType mapType);
        void Visit(PatternType patternType);
        void Visit(OnScreenEnteredType onScreenEnteredType);
        void Visit(EntityType entityType);
        void Visit(DataType dataType);
        void Visit(EntitiesType entitiesType);
        void Visit(ExitsType exitsType);
        void Visit(GameObjectContent gameObjectContent);
        void Visit(StatementBlock statementBlock);
        void Visit(FactorExpression factorExpression);
        void Visit(FactorOperation factorOperation);
        void Visit(SumExpression sumExpression);
        void Visit(SumOperation sumOperation);
        void Visit(TerminalExpression terminalExpression);
        void Visit(MovePatternType movePatternType);
        void Visit(ScreenType gameObjectContent);
        void Visit(IfStatement ifStatement);
        void Visit(RepeatNode repeatNode);
        void Visit(StatementExpression statementExpression);
        void Visit(MemberAccess memberAccess);
        void Visit(FloatValue floatValue);
        void Visit(IdentifierValue identifierValue);
        void Visit(IntValue intValue);
        void Visit(Array array);
    }
}