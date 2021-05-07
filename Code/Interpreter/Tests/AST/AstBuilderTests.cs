using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.Ast.Visitors;
using NUnit.Framework;

namespace Tests.AST
{
    [TestFixture]
    internal sealed class AstBuilderTests : ICompleteVisitor
    {
        private IParseTree parseTree;
        
        private const string TestCode =
            "Screen SampleScreen1" + // Test GameObject
            "{" +
            "   Map" + // Test GameObjectContent & GameObjectContentType
            "   {" +
            "       Size(30, 24);" + // Test function invocation
            "       SomeVar1 = 2.0;" + // Test assignment & floats
            "       { " + // Test statement block
            "           SomeVar2 = 3 + 3 / 3;" + // Test expressions & integers
            "           x = SomeVar2;" + // Test identifier values
            "       }" +
            "       let = SampleScreen1.Exits.exit1;" + // Test member access 
            "       arr = [1, 2, 3];" + // Test arrays
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(Skeleton1, [4, 5]);" +
            "   }" +
            "}";

        [SetUp]
        public void Setup()
        {
            ICharStream stream = CharStreams.fromString(TestCode);
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            parseTree = new DazelParser(tokens) {BuildParseTree = true}.start();
        }
        
        [Test]
        public void BuildAst_BuildsAst_IsCorrect()
        {
            AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTree);
            
            foreach (GameObject gameObject in ast.Root.GameObjects.Values)
            {
                Visit(gameObject);
            }
        }

        
        public void Visit(GameObject gameObject)
        {
            // Line 1
            Assert.That(gameObject.Type is Screen, "gameObject.Type is ScreenType");
            Assert.That(gameObject.Identifier == "SampleScreen1", "gameObject.Identifier == 'SampleScreen1'");
            
            // Contents: Map, Entities
            Assert.That(gameObject.Contents[0].Type is MapType, "gameObject.Contents[0].Type is MapType");
            Assert.That(gameObject.Contents[1].Type is EntitiesType, "gameObject.Contents[1].Type is EntitiesType");

            // Map has 12 top-level statements (some statement blocks have nested statements)
            Assert.That(gameObject.Contents[0].Statements.Count == 5, $"gameObject.Contents[0].Statements.Count == 5." +
                                                                       $"Found {gameObject.Contents[0].Statements.Count}");
            
            // Entities has 1 top-level statement
            Assert.That(gameObject.Contents[1].Statements.Count == 1, $"gameObject.Contents[1].Statements.Count == 1" +
                                                                      $"Found {gameObject.Contents[1].Statements.Count}");
            
            gameObject.Contents.ForEach(Visit);
        }

        #region Types

        public void Visit(MapType mapType) { }

        public void Visit(PatternType patternType) { }

        public void Visit(OnScreenEnteredType onScreenEnteredType) { }

        public void Visit(Entity entity) { }

        public void Visit(DataType dataType) { }

        public void Visit(EntitiesType entitiesType) { }

        public void Visit(ExitsType exitsType) { }
        #endregion

        public void Visit(GameObjectContent gameObjectContent)
        {
            gameObjectContent.Statements.ForEach(statement =>
            {
                statement.Accept(this);
            });
        }

        public void Visit(StatementBlock statementBlock)
        {
            // There is one statement block with 2 statements inside.
            Assert.That(statementBlock.Statements.Count == 2);
            
            statementBlock.Statements.ForEach(statement => statement.Accept(this));
        }

        public void Visit(FactorExpression factorExpression) { }
        public void Visit(FactorOperation factorOperation) { }

        public void Visit(SumExpression sumExpression) { }

        public void Visit(SumOperation sumOperation) { }

        public void Visit(TerminalExpression terminalExpression) { }

        public void Visit(MovePattern movePattern) { }

        public void Visit(Screen screen) { }

        public void Visit(IfStatement ifStatement) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(FunctionInvocation functionInvocation)
        {
            Assert.That(
                functionInvocation.Identifier == "Size" ||
                functionInvocation.Identifier == "SpawnEntity"
                );
            
            switch (functionInvocation.Identifier)
            {
                case "Size":
                    Assert.That(functionInvocation.Parameters[0] is IntValue {Value: 30});
                    Assert.That(functionInvocation.Parameters[1] is IntValue {Value: 24});
                    break;
                case "SpawnEntity":
                    Assert.That(functionInvocation.Parameters[0] is IdentifierValue {Value: "Skeleton1"});
                    Assert.That(functionInvocation.Parameters[1] is ArrayNode array && 
                                array.Values[0] is IntValue {Value: 4} &&
                                array.Values[1] is IntValue {Value: 5});
                    break;
            }
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            Assert.That(
                assignmentNode.Identifier == "SomeVar1" ||
                assignmentNode.Identifier == "SomeVar2" ||
                assignmentNode.Identifier == "x" ||
                assignmentNode.Identifier == "let" ||
                assignmentNode.Identifier == "arr"
                );

            switch (assignmentNode.Identifier)
            {
                // SomeVar1 = 2.0
                case "SomeVar1":
                    Assert.That(assignmentNode.Expression is FloatValue {Value: 2.0f});
                    break;
                // SomeVar2 = 3 + 3 / 3
                case "SomeVar2":
                    Assert.That(assignmentNode.Expression is SumExpression
                        {
                            Left: IntValue {Value: 3},
                            Operation: {Operation: '+'},
                            Right: FactorExpression {
                                Left: IntValue {Value: 3},
                                Operation: {Operation: '/'},
                                Right: IntValue {Value: 3},
                            }
                        }
                    );
                    break;
                // x = SomeVar3
                case "x":
                    Assert.That(assignmentNode.Expression is IdentifierValue {Value: "SomeVar2"});
                    break;
                // let = SampleScreen1.Exits.exit1
                case "let":
                    Assert.That(assignmentNode.Expression is MemberAccess me &&
                                me.Identifiers[0] == "SampleScreen1" && 
                                me.Identifiers[1] == "Exits" && 
                                me.Identifiers[2] == "exit1"
                    );
                    break;
                // arr = [1, 2, 3]
                case "arr":
                    Assert.That(assignmentNode.Expression is ArrayNode arr &&
                                arr.Values[0] is IntValue {Value: 1} &&
                                arr.Values[1] is IntValue {Value: 2} &&
                                arr.Values[2] is IntValue {Value: 3}
                    );
                    break;
            }
        }

        public void Visit(MemberAccess memberAccess) { }

        public void Visit(FloatValue floatValue) { }

        public void Visit(IdentifierValue identifierValue) { }

        public void Visit(IntValue intValue) { }

        public void Visit(ArrayNode arrayNode) { }
        public void Visit(StringNode stringNode) { }

        public void Visit(ExitValue exitValue) {
        }
    }
}