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
        }

        public void Visit(MapType mapType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(PatternType patternType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(EntityType entityType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(DataType dataType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(EntitiesType entitiesType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ExitsType exitsType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(StatementBlock statementBlock)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FactorExpression factorExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FactorOperation factorOperation)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(SumExpression sumExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(SumOperation sumOperation)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TerminalExpression terminalExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(MovePatternType movePatternType)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ScreenType gameObjectContent)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IfStatement ifStatement)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(RepeatNode repeatNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(StatementExpression statementExpression)
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