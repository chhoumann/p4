using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Array = Interpreter.Ast.Nodes.ExpressionNodes.Array;

namespace Interpreter.SemanticAnalysis
{
    public sealed class SymbolTableBuilder : IVisitor
    {
        public Scope ScopeRoot { get; private set; }
        private Scope currentScope;

        public SymbolTableBuilder(GameObject astRoot)
        {
            Visit(astRoot);
            SymbolTable.Instance.Scopes.Add(ScopeRoot);
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
            currentScope.Type = typeof(MapType);
            currentScope.Identifier = nameof(MapType);
        }

        public void Visit(PatternType patternType)
        {
            currentScope.Type = typeof(PatternType);
            currentScope.Identifier = nameof(PatternType);
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
            currentScope.Type = typeof(OnScreenEnteredType);
            currentScope.Identifier = nameof(OnScreenEnteredType);
        }

        public void Visit(EntityType entityType)
        {
            currentScope.Type = typeof(EntityType);
            currentScope.Identifier = nameof(EntityType);
        }

        public void Visit(DataType dataType)
        {
            currentScope.Type = typeof(DataType);
            currentScope.Identifier = nameof(DataType);
        }

        public void Visit(EntitiesType entitiesType)
        {
            currentScope.Type = typeof(EntitiesType);
            currentScope.Identifier = nameof(EntitiesType);
        }

        public void Visit(ExitsType exitsType)
        {
            currentScope.Type = typeof(ExitsType);
            currentScope.Identifier = nameof(ExitsType);
        }
        
        public void Visit(MovePatternType movePatternType)
        {
            currentScope.Type = typeof(MovePatternType);
            currentScope.Identifier = nameof(MovePatternType);

        }

        public void Visit(ScreenType screenType)
        {
            currentScope.Type = typeof(ScreenType);
            currentScope.Identifier = nameof(ScreenType);
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            currentScope = ScopeRoot.OpenChildScope("");
            Scope thisLevel = currentScope;
            
            gameObjectContent.Type.Accept(this);

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
                    Type = typeof(FunctionInvocation)
                });

                foreach (Value param in functionInvocation.Parameters)
                {
                    param.Accept(this);
                }
            }

            if (statementExpression is AssignmentNode assignmentNode)
            {
                currentScope.EnterSymbol(new ScopeRow()
                {
                    Identifier = assignmentNode.Identifier,
                    IsDeclaration = currentScope.IsDeclaration(assignmentNode.Identifier),
                    Type = typeof(AssignmentNode) // This should be evaluated and set to a proper type.
                });

                assignmentNode.Expression.Accept(this);
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
            string joinedIdentifiers = string.Join('.', memberAccess.Identifiers);
            
            currentScope.EnterSymbol(new ScopeRow()
            {
                Identifier = joinedIdentifiers,
                Type = typeof(MemberAccess),
                IsDeclaration = currentScope.IsDeclaration(joinedIdentifiers),
            });
        }

        public void Visit(FloatValue floatValue) { }

        public void Visit(IdentifierValue identifierValue)
        {
            currentScope.EnterSymbol(new ScopeRow()
            {
                Identifier = identifierValue.Value,
                IsDeclaration = currentScope.IsDeclaration(identifierValue.Value),
                Type = typeof(IdentifierValue)
            });
        }

        public void Visit(IntValue intValue) { }

        public void Visit(Array array) { }
    }
}