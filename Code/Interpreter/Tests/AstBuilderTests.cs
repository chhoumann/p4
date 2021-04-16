using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public sealed class AstBuilderTests : IVisitor
    {
        private IParseTree parseTree;
        private const string TestCodePath = "./dazel_test_code.txt";

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromPath(TestCodePath);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();
        }
        
        [Test]
        public void BuildAst_BuildsAst_IsCorrect()
        {
            var ast = new AstBuilder().BuildAst(parseTree);
            
            Visit(ast.Root);
        }

        
        public void Visit(GameObject gameObject)
        {
            Assert.That(gameObject.Type is ScreenType);
            
            gameObject.Contents.ForEach(Visit);
        }

        #region Types

        public void Visit(MapType mapType) { }

        public void Visit(PatternType patternType) { }

        public void Visit(OnScreenEnteredType onScreenEnteredType) { }

        public void Visit(EntityType entityType) { }

        public void Visit(DataType dataType) { }

        public void Visit(EntitiesType entitiesType) { }

        public void Visit(ExitsType exitsType) { }
        #endregion

        public void Visit(GameObjectContent gameObjectContent)
        {
            Assert.That(
                gameObjectContent.Type is DataType ||
                gameObjectContent.Type is EntitiesType ||
                gameObjectContent.Type is ExitsType ||
                gameObjectContent.Type is MapType ||
                gameObjectContent.Type is OnScreenEnteredType ||
                gameObjectContent.Type is PatternType
            );
            
            gameObjectContent.Statements.ForEach(statement =>
            {
                statement.Accept(this);
            });
        }

        public void Visit(StatementBlock statementBlock)
        {
            // If a block is created, there has to be statements inside.
            Assert.That(statementBlock.Statements.Count > 0);

            statementBlock.Statements.ForEach(statement => statement.Accept(this));
        }

        public void Visit(FactorExpression factorExpression)
        {
            factorExpression.Left.Accept(this);
            factorExpression.Operation.Accept(this);
            factorExpression.Right.Accept(this);
        }

        public void Visit(FactorOperation factorOperation)
        {
            Assert.That(
                factorOperation.Operation == DazelLexer.MULTIPLICATION_OP ||
                factorOperation.Operation == DazelLexer.DIVISION_OP
                );
        }

        public void Visit(SumExpression sumExpression)
        {
            sumExpression.Left.Accept(this);
            sumExpression.Operation.Accept(this);
            sumExpression.Right.Accept(this);
        }

        public void Visit(SumOperation sumOperation)
        {
            Assert.That(
                sumOperation.Operation == DazelLexer.PLUS_OP ||
                sumOperation.Operation == DazelLexer.MINUS_OP
                );
        }

        public void Visit(TerminalExpression terminalExpression)
        {
            terminalExpression.Child.Accept(this);
        }

        public void Visit(MovePatternType movePatternType) { }

        public void Visit(ScreenType gameObjectContent) { }

        public void Visit(IfStatement ifStatement) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(StatementExpression statementExpression)
        {
            statementExpression.Accept(this);
        }

        public void Visit(FunctionInvocation functionInvocation)
        {
            
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(MemberAccess memberAccess)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FloatValue floatValue)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IdentifierValue identifierValue)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IntValue intValue)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Array array)
        {
            throw new System.NotImplementedException();
        }
    }
}