using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.Ast.Visitors;

namespace Interpreter.SemanticAnalysis
{
    public sealed class TypeChecker : SemanticAnalysis, IGameObjectVisitor, IStatementVisitor
    {
        private readonly AbstractSyntaxTree ast;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }
        
        public void Visit(GameObject gameObject)
        {
            EnvironmentStack.Push(new SymbolTable<SymbolTableEntry>());
            
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }

            EnvironmentStack.Pop();
        }

        public void Visit(Entity entity)
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
            SymbolType expressionType = new ExpressionTypeChecker(ast, currentSymbolTable).GetType(assignmentNode.Expression);

            VariableSymbolTableEntry entry = new(expressionType);
        }
    }
}