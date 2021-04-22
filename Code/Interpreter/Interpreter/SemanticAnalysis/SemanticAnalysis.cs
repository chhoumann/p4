using System.Collections.Generic;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.SemanticAnalysis
{
    public abstract class SemanticAnalysis
    {
        protected Stack<SymbolTable<SymbolTableEntry>> environmentStack = new();
    }

    public class TypeChecker : SemanticAnalysis, IVisitor
    {
        public void Visit(GameObject gameObject)
        {
            environmentStack.Push(new SymbolTable<SymbolTableEntry>());
            
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }

            environmentStack.Pop();
        }

        public void Visit(MapType mapType)
        {
        }

        public void Visit(PatternType patternType)
        {
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
        }

        public void Visit(EntityType entityType)
        {
        }

        public void Visit(DataType dataType)
        {
        }

        public void Visit(EntitiesType entitiesType)
        {
        }

        public void Visit(ExitsType exitsType)
        {
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                Visit(statementNode);
            }
        }

        public void Visit(StatementBlock statementBlock)
        {
        }

        public void Visit(FactorExpression factorExpression)
        {
        }

        public void Visit(FactorOperation factorOperation)
        {
        }

        public void Visit(SumExpression sumExpression)
        {
        }

        public void Visit(SumOperation sumOperation)
        {
        }

        public void Visit(TerminalExpression terminalExpression)
        {
        }

        public void Visit(MovePatternType movePatternType)
        {
        }

        public void Visit(ScreenType gameObjectContent)
        {
        }

        public void Visit(IfStatement ifStatement)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(StatementExpression statementExpression)
        {
        }

        public void Visit(MemberAccess memberAccess)
        {
        }

        public void Visit(FloatValue floatValue)
        {
        }

        public void Visit(IdentifierValue identifierValue)
        {
        }

        public void Visit(IntValue intValue)
        {
        }

        public void Visit(Array array)
        {
        }
    }
}