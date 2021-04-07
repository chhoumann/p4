using System;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Array = Interpreter.Ast.Nodes.ExpressionNodes.Array;

namespace Interpreter.Ast
{
    public class SymbolTableBuilder : IVisitor
    {
        public Scope ScopeRoot { get; set; }
        private Scope currentScope;

        public SymbolTableBuilder(GameObject astRoot)
        {
            Visit(astRoot);
            ScopeRoot.Print(0);
        }


        public void Visit(GameObject gameObject)
        {
            ScopeRoot = new Scope(gameObject.Identifier, null);
            currentScope = ScopeRoot;
            gameObject.Type.Accept(this);
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }
        }

        public void Visit(MapType mapType)
        {
            currentScope.Type = "Map";
        }

        public void Visit(PatternType patternType)
        {
            currentScope.Type = "Pattern";
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
            currentScope.Type = "OnScreenEntered";
        }

        public void Visit(EntityType entityType)
        {
            currentScope.Type = "Entity";
        }

        public void Visit(DataType dataType)
        {
            currentScope.Type = "Data";
        }

        public void Visit(EntitiesType entitiesType)
        {
            currentScope.Type = "Entities";
        }

        public void Visit(ExitsType exitsType)
        {
            currentScope.Type = "Exits";
        }
        
        public void Visit(MovePatternType movePatternType)
        {
            currentScope.Type = "MovePatternType";
        }

        public void Visit(ScreenType screenType)
        {
            currentScope.Type = "Screen";
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            currentScope = ScopeRoot.OpenChildScope("");
            Scope thisLevel = currentScope;
            gameObjectContent.Type.Accept(this);
            currentScope.Identifier = currentScope.Type;
            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
                currentScope = thisLevel;
            }
        }

        public void Visit(StatementBlock statementBlock)
        {
            Scope scope = currentScope.OpenChildScope("");
            currentScope = scope;
            foreach (StatementNode statementBlockStatement in statementBlock.Statements)
            {
                statementBlockStatement.Accept(this);
            }
        }
        
        public void Visit(StatementExpression statementExpression)
        {
            if (statementExpression is FunctionInvocation functionInvocation)
            {
                currentScope.EnterSymbol(new ScopeRow()
                {
                    Identifier = functionInvocation.Identifier,
                    IsDeclaration = false,
                    Type = "Function"
                });
                
                // functionInvocation.Parameters
            }

            if (statementExpression is AssignmentNode assignmentNode)
            {
                currentScope.EnterSymbol(new ScopeRow()
                {
                    Identifier = assignmentNode.Identifier,
                    IsDeclaration = currentScope.IsDeclaration(assignmentNode.Identifier),
                    Type = ""
                });
            }
        }

        public void Visit(FactorExpression factorExpression)
        {
            factorExpression.Left.Accept(this);
            factorExpression.Right.Accept(this);
        }

        public void Visit(FactorOperation factorOperation) { }

        public void Visit(SumExpression sumExpression)
        {
            sumExpression.Left.Accept(this);
            sumExpression.Right.Accept(this);
        }

        public void Visit(SumOperation sumOperation) { }

        public void Visit(TerminalExpression terminalExpression)
        {
            terminalExpression.Child.Accept(this);
        }

        public void Visit(IfStatement ifStatement) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(MemberAccess memberAccess)
        {
            
        }

        public void Visit(FloatValue floatValue) { }

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