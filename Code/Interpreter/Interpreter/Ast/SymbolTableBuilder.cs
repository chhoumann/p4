using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    public class SymbolTableBuilder : IVisitor
    {
        public void Visit(Array array) { }

        public void Visit(FactorExpression factorExpression) { }

        public void Visit(FactorOperation factorOperation) { }

        public void Visit(FloatValue floatValue) { }

        public void Visit(IdentifierValue identifierValue) { }

        public void Visit(IntValue intValue) { }

        public void Visit(MemberAccess memberAccess) { }

        public void Visit(SumExpression sumExpression) {}

        public void Visit(SumOperation sumOperation) { }

        public void Visit(TerminalExpression terminalExpression) { }

        public void Visit(EntityType entityType)
        {
            // SymbolTable.Instance[0].
        }

        public void Visit(GameObject gameObject) { }

        public void Visit(GameObjectContent gameObjectContent) { }

        public void Visit(MovePatternType movePatternType) { }

        public void Visit(ScreenType gameObjectContent) { }

        public void Visit(IfStatement ifStatement) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(StatementExpression statementExpression)
        {
            if (statementExpression is AssignmentNode assignmentNode)
            {
                
            }
        }

        public void Visit(DataType dataType) { }

        public void Visit(EntitiesType entitiesType) { }

        public void Visit(ExitsType exitsType) { }

        public void Visit(MapType mapType) { }

        public void Visit(OnScreenEnteredType onScreenEnteredType) { }

        public void Visit(PatternType patternType) { }
        public void Visit(StatementBlock statementBlock)
        {
            
        }
    }
}