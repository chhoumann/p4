﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Interpreter.Ast.Nodes.StatementNodes;
using Dazel.Interpreter.Ast.Visitors;
using NUnit.Framework;

namespace Dazel.Tests.EditMode.AST
{
    [TestFixture]
    public sealed class AstBuilderTests : ICompleteVisitor
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
            "       let = Player.Health;" + // Test member access 
            "       arr = [1, 2, 3];" + // Test arrays
            "   }" +
            "" +
            "   Entities" +
            "   {" +
            "       SpawnEntity(\"Skeleton1\", [4, 5]);" +
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
            
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                Visit(gameObject);
            }
        }

        
        public void Visit(GameObjectNode gameObjectNode)
        {
            // Line 1
            Assert.That(gameObjectNode.TypeNode is ScreenNode, "gameObject.Type is ScreenType");
            Assert.That(gameObjectNode.Identifier == "SampleScreen1", "gameObject.Identifier == 'SampleScreen1'");
            
            // Contents: Map, Entities
            Assert.That(gameObjectNode.Contents[0].TypeNode is MapTypeNode, "gameObject.Contents[0].Type is MapType");
            Assert.That(gameObjectNode.Contents[1].TypeNode is EntitiesTypeNodeNode, "gameObject.Contents[1].Type is EntitiesType");

            // Map has 12 top-level statements (some statement blocks have nested statements)
            Assert.That(gameObjectNode.Contents[0].Statements.Count == 5, $"gameObject.Contents[0].Statements.Count == 5." +
                                                                       $"Found {gameObjectNode.Contents[0].Statements.Count}");
            
            // Entities has 1 top-level statement
            Assert.That(gameObjectNode.Contents[1].Statements.Count == 1, $"gameObject.Contents[1].Statements.Count == 1" +
                                                                      $"Found {gameObjectNode.Contents[1].Statements.Count}");
            
            gameObjectNode.Contents.ForEach(Visit);
        }

        #region Types

        public void Visit(MapTypeNode mapTypeNode) { }

        public void Visit(PatternTypeNode patternTypeNode) { }

        public void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode) { }

        public void Visit(EntityNode entityNode) { }

        public void Visit(DataTypeNodeNode dataTypeNodeNode) { }

        public void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode) { }

        public void Visit(ExitsTypeNodeNode exitsTypeNodeNode) { }
        #endregion

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
            gameObjectContentNode.Statements.ForEach(statement =>
            {
                statement.Accept(this);
            });
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            // There is one statement block with 2 statements inside.
            Assert.That(statementBlockNode.Statements.Count == 2);
            
            statementBlockNode.Statements.ForEach(statement => statement.Accept(this));
        }

        public void Visit(FactorExpressionNode factorExpressionNode) { }
        public void Visit(FactorOperationNode factorOperationNode) { }

        public void Visit(SumExpressionNode sumExpressionNode) { }

        public void Visit(SumOperationNode sumOperationNode) { }

        public void Visit(TerminalExpressionNode terminalExpressionNode) { }

        public void Visit(MovePatternNode movePatternNode) { }

        public void Visit(ScreenNode screenNode) { }

        public void Visit(IfStatementNode ifStatementNode) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            Assert.That(
                functionInvocationNode.Identifier == "Size" ||
                functionInvocationNode.Identifier == "SpawnEntity"
                );
            
            switch (functionInvocationNode.Identifier)
            {
                case "Size":
                    Assert.That(functionInvocationNode.Parameters[0] is IntValueNode {Value: 30});
                    Assert.That(functionInvocationNode.Parameters[1] is IntValueNode {Value: 24});
                    break;
                case "SpawnEntity":
                    Assert.That(functionInvocationNode.Parameters[0] is StringNode {Value: "Skeleton1"});
                    Assert.That(functionInvocationNode.Parameters[1] is ArrayNode array && 
                                array.Values[0] is IntValueNode {Value: 4} &&
                                array.Values[1] is IntValueNode {Value: 5});
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
                    Assert.That(assignmentNode.Expression is FloatValueNode {Value: 2.0f});
                    break;
                // SomeVar2 = 3 + 3 / 3
                case "SomeVar2":
                    Assert.That(assignmentNode.Expression is SumExpressionNode
                        {
                            Left: IntValueNode {Value: 3},
                            OperationNode: {Operation: '+'},
                            Right: FactorExpressionNode {
                                Left: IntValueNode {Value: 3},
                                OperationNode: {Operation: '/'},
                                Right: IntValueNode {Value: 3},
                            }
                        }
                    );
                    break;
                // x = SomeVar3
                case "x":
                    Assert.That(assignmentNode.Expression is IdentifierValueNode {Value: "SomeVar2"});
                    break;
                // let = SampleScreen1.Exits.exit1
                case "let":
                    Assert.That(assignmentNode.Expression is MemberAccessNode me &&
                                me.Identifiers[0] == "Player" && 
                                me.Identifiers[1] == "Health" 
                    );
                    break;
                // arr = [1, 2, 3]
                case "arr":
                    Assert.That(assignmentNode.Expression is ArrayNode arr &&
                                arr.Values[0] is IntValueNode {Value: 1} &&
                                arr.Values[1] is IntValueNode {Value: 2} &&
                                arr.Values[2] is IntValueNode {Value: 3}
                    );
                    break;
            }
        }

        public void Visit(MemberAccessNode memberAccessNode) { }

        public void Visit(FloatValueNode floatValueNode) { }

        public void Visit(IdentifierValueNode identifierValueNode) { }

        public void Visit(IntValueNode intValueNode) { }

        public void Visit(ArrayNode arrayNode) { }
        public void Visit(StringNode stringNode) { }

        public void Visit(ExitValueNode exitValueNode) {
        }
    }
}