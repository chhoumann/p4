using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.Ast.Nodes.StatementNodes;
using Dazel.Interpreter.Ast.Visitors;

namespace Dazel.Interpreter.SemanticAnalysis
{
    public sealed class TypeChecker : SemanticAnalysis, IGameObjectVisitor, IStatementVisitor
    {
        private readonly AbstractSyntaxTree ast;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }

        public void Visit(GameObjectNode gameObjectNode)
        {
            OpenScope();

            foreach (GameObjectContentNode gameObjectContent in gameObjectNode.Contents) Visit(gameObjectContent);

            CloseScope();
        }

        public void Visit(EntityNode entityNode)
        {
        }

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
            OpenScope();

            foreach (StatementNode statementNode in gameObjectContentNode.Statements) statementNode.Accept(this);

            CloseScope();
        }

        public void Visit(MovePatternNode movePatternNode)
        {
        }

        public void Visit(ScreenNode screenNode)
        {
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            OpenScope();

            foreach (StatementNode statement in statementBlockNode.Statements) statement.Accept(this);

            CloseScope();
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            functionInvocationNode.Function.Setup(functionInvocationNode.Parameters, ast);
            FunctionSymbolTableEntry entry = new FunctionSymbolTableEntry(functionInvocationNode.ReturnType, functionInvocationNode.Parameters);

            EnvironmentStack.Peek().AddOrUpdateSymbol(functionInvocationNode.Identifier, entry);
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