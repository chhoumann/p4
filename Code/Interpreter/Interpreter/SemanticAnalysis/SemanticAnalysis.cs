using System.Collections.Generic;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.Ast.Nodes.ValueNodes;

namespace Interpreter.SemanticAnalysis
{
    public abstract class SemanticAnalysis
    {
        protected readonly Stack<SymbolTable<SymbolTableEntry>> EnvironmentStack = new();
    }

    public class TypeChecker : SemanticAnalysis, IVisitor
    {
        public void Visit(GameObject gameObject)
        {
            EnvironmentStack.Push(new SymbolTable<SymbolTableEntry>());
            
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }

            EnvironmentStack.Pop();
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

        public void Visit(Entity entity)
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
            EnvironmentStack.Push(new SymbolTable<SymbolTableEntry>());

            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
            }

            EnvironmentStack.Pop();
        }

        public void Visit(StatementBlock statementBlock)
        {
            EnvironmentStack.Push(new SymbolTable<SymbolTableEntry>());
            
            foreach (StatementNode statement in statementBlock.Statements)
            {
                statement.Accept(this);
            }

            EnvironmentStack.Pop();
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

        public void Visit(MovePattern movePattern)
        {
        }

        public void Visit(Screen screen)
        {
        }

        public void Visit(IfStatement ifStatement)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(FunctionInvocation functionInvocation)
        {
            FunctionSymbolTableEntry entry = new(functionInvocation.ReturnType, functionInvocation.Parameters);
            
            EnvironmentStack.Peek().AddOrUpdateSymbol(functionInvocation.Identifier, entry);
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            SymbolTable<SymbolTableEntry> currentSymbolTable = EnvironmentStack.Peek();
            SymbolType expressionType = new ExpressionTypeChecker(currentSymbolTable).GetType(assignmentNode.Expression);

            VariableSymbolTableEntry entry = new(expressionType);
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

        public void Visit(ArrayNode arrayNode)
        {
        }
    }
}