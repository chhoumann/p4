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
        public SymbolTable SymbolTableRoot { get; private set; }
        private SymbolTable currentSymbolTable;

        public SymbolTableBuilder(GameObject astRoot)
        {
            Visit(astRoot);
            
            GlobalSymbolTable.SymbolTables.Add(SymbolTableRoot.Identifier, SymbolTableRoot);
            SymbolTableRoot.Print(0);
        }
        
        public void Visit(GameObject gameObject)
        {
            SymbolTableRoot = new SymbolTable(gameObject.Identifier, null);
            currentSymbolTable = SymbolTableRoot;
            
            gameObject.Type.Accept(this);
            
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }
        }

        public void Visit(MapType mapType)
        {
            currentSymbolTable.Type = typeof(MapType);
            currentSymbolTable.Identifier = nameof(MapType);
        }

        public void Visit(PatternType patternType)
        {
            currentSymbolTable.Type = typeof(PatternType);
            currentSymbolTable.Identifier = nameof(PatternType);
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
            currentSymbolTable.Type = typeof(OnScreenEnteredType);
            currentSymbolTable.Identifier = nameof(OnScreenEnteredType);
        }

        public void Visit(EntityType entityType)
        {
            currentSymbolTable.Type = typeof(EntityType);
            currentSymbolTable.Identifier = nameof(EntityType);
        }

        public void Visit(DataType dataType)
        {
            currentSymbolTable.Type = typeof(DataType);
            currentSymbolTable.Identifier = nameof(DataType);
        }

        public void Visit(EntitiesType entitiesType)
        {
            currentSymbolTable.Type = typeof(EntitiesType);
            currentSymbolTable.Identifier = nameof(EntitiesType);
        }

        public void Visit(ExitsType exitsType)
        {
            currentSymbolTable.Type = typeof(ExitsType);
            currentSymbolTable.Identifier = nameof(ExitsType);
        }
        
        public void Visit(MovePatternType movePatternType)
        {
            currentSymbolTable.Type = typeof(MovePatternType);
            currentSymbolTable.Identifier = nameof(MovePatternType);

        }

        public void Visit(ScreenType screenType)
        {
            currentSymbolTable.Type = typeof(ScreenType);
            currentSymbolTable.Identifier = nameof(ScreenType);
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            currentSymbolTable = SymbolTableRoot.OpenChildScope("");
            SymbolTable thisLevel = currentSymbolTable;
            
            gameObjectContent.Type.Accept(this);

            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
                currentSymbolTable = thisLevel;
            }
        }

        public void Visit(StatementBlock statementBlock)
        {
            SymbolTable symbolTable = currentSymbolTable.OpenChildScope("");
            currentSymbolTable = symbolTable;
            
            foreach (StatementNode statementBlockStatement in statementBlock.Statements)
            {
                statementBlockStatement.Accept(this);
            }
        }
        
        public void Visit(StatementExpression statementExpression)
        {
            if (statementExpression is FunctionInvocation functionInvocation)
            {
                currentSymbolTable.EnterSymbol(new SymbolTableEntry()
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
                currentSymbolTable.EnterSymbol(new SymbolTableEntry
                {
                    Identifier = assignmentNode.Identifier,
                    IsDeclaration = currentSymbolTable.IsDeclaration(assignmentNode.Identifier),
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
            
            currentSymbolTable.EnterSymbol(new SymbolTableEntry()
            {
                Identifier = joinedIdentifiers,
                Type = typeof(MemberAccess),
                IsDeclaration = currentSymbolTable.IsDeclaration(joinedIdentifiers),
            });
        }

        public void Visit(FloatValue floatValue) { }

        public void Visit(IdentifierValue identifierValue)
        {
            currentSymbolTable.EnterSymbol(new SymbolTableEntry()
            {
                Identifier = identifierValue.Value,
                IsDeclaration = currentSymbolTable.IsDeclaration(identifierValue.Value),
                Type = typeof(IdentifierValue)
            });
        }

        public void Visit(IntValue intValue) { }

        public void Visit(Array array) { }
    }
}