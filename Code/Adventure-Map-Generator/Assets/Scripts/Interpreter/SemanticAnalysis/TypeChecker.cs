using P4.MapGenerator.Interpreter.Ast;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;
using P4.MapGenerator.Interpreter.Ast.Visitors;

namespace P4.MapGenerator.Interpreter.SemanticAnalysis
{
    public sealed class TypeChecker : SemanticAnalysis, IGameObjectVisitor, IStatementVisitor
    {
        private readonly AbstractSyntaxTree ast;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }

        public void Visit(DGameObject gameObject)
        {
            OpenScope();

            foreach (GameObjectContent gameObjectContent in gameObject.Contents) Visit(gameObjectContent);

            CloseScope();
        }

        public void Visit(Entity entity)
        {
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            OpenScope();

            foreach (StatementNode statementNode in gameObjectContent.Statements) statementNode.Accept(this);

            CloseScope();
        }

        public void Visit(MovePattern movePattern)
        {
        }

        public void Visit(Screen screen)
        {
        }

        public void Visit(StatementBlock statementBlock)
        {
            OpenScope();

            foreach (StatementNode statement in statementBlock.Statements) statement.Accept(this);

            CloseScope();
        }

        public void Visit(IfStatement ifStatement)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(FunctionInvocation functionInvocation)
        {
            FunctionSymbolTableEntry entry = new FunctionSymbolTableEntry(functionInvocation.ReturnType, functionInvocation.Parameters);

            EnvironmentStack.Peek().AddOrUpdateSymbol(functionInvocation.Identifier, entry);
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            SymbolTable<SymbolTableEntry> currentSymbolTable = EnvironmentStack.Peek();
            SymbolType expressionType = new ExpressionTypeChecker(ast, currentSymbolTable).GetType(assignmentNode.Expression);

            VariableSymbolTableEntry entry = new VariableSymbolTableEntry(expressionType);
            currentSymbolTable.AddOrUpdateSymbol(assignmentNode.Identifier, entry);
        }
    }
}