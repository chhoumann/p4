using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Compiler.Ast
{
    public sealed class AstBuilder : EnvironmentStore, IAstBuilder
    {
        protected override string GameObjectIdentifier { get; set; }

        public AbstractSyntaxTree BuildAst(IEnumerable<IParseTree> parseTrees)
        {
            Dictionary<string, GameObjectNode> gameObjects = new Dictionary<string, GameObjectNode>();
            
            foreach (IParseTree parseTree in parseTrees)
            {
                DazelParser.GameObjectContext gameObjectContext = parseTree.GetChild(0) as DazelParser.GameObjectContext;
                GameObjectIdentifier = gameObjectContext.GetChild(1).GetText();
                OpenScope();
                GameObjectNode gameObjectNode = VisitGameObject(gameObjectContext);
                CloseScope();
                gameObjects.Add(gameObjectNode.Identifier, gameObjectNode);
            }

            RootNode root = new RootNode()
            {
                GameObjects = gameObjects
            };
            
            return new AbstractSyntaxTree(root);
        }

        public AbstractSyntaxTree BuildAst(IParseTree parseTree)
        {
            return BuildAst(new[] {parseTree});
        }

        #region GameObject

        public GameObjectNode VisitGameObject(DazelParser.GameObjectContext context)
        {
            GameObjectTypeNode typeNode;
            
            switch (context.gameObjectType.Type)
            {
                case DazelLexer.SCREEN:
                    typeNode = new ScreenNode();
                    break;
                case DazelLexer.ENTITY:
                    typeNode = new EntityNode();
                    break;
                case DazelLexer.MOVE_PATTERN:
                    typeNode = new MovePatternNode();
                    break;
                default:
                    throw new ArgumentException("Type is not a GameObjectType!");
            }

            GameObjectIdentifier = context.GetChild(1).GetText();

            GameObjectNode gameObjectNode = new GameObjectNode()
            {
                Token = context.Start,
                Identifier = GameObjectIdentifier,
                TypeNode = typeNode,
                Contents = VisitGameObjectContents(context.gameObjectBlock().gameObjectContents())
            };
            

            return gameObjectNode;
        }

        public List<GameObjectContentNode> VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            OpenScope();

            List<GameObjectContentNode> contents = new List<GameObjectContentNode>();

            if (context.gameObjectContent() == null) return contents;

            contents.Add(VisitGameObjectContent(context.gameObjectContent()));
            
            if (context.gameObjectContents() != null)
            {
                contents.AddRange(VisitGameObjectContents(context.gameObjectContents()));
            }
            
            CloseScope();

            return contents;
        }

        public GameObjectContentNode VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {

            GameObjectContentTypeNode gameObjectContentTypeNode;
            
            switch (context.gameObjectContentType.Type)
            {
                case DazelLexer.MAP:
                    gameObjectContentTypeNode = new MapTypeNode()
                    {
                        Token = context.Start
                    };
                    break;
                case DazelLexer.ONSCREENENTERED:
                    gameObjectContentTypeNode = new OnScreenEnteredTypeNode()
                    {
                        Token = context.Start
                    };
                    break;
                case DazelLexer.ENTITIES:
                    gameObjectContentTypeNode = new EntitiesTypeNodeNode()
                    {
                        Token = context.Start
                    };
                    break;
                case DazelLexer.EXITS:
                    gameObjectContentTypeNode = new ExitsTypeNodeNode()
                    {
                        Token = context.Start
                    };
                    break;
                case DazelLexer.DATA:
                    gameObjectContentTypeNode = new DataTypeNodeNode()
                    {
                        Token = context.Start
                    };
                    break;
                case DazelLexer.PATTERN:
                    gameObjectContentTypeNode = new PatternTypeNode()
                    {
                        Token = context.Start
                    };
                    break;
                default:
                    throw new ArgumentException("Invalid content type.");
            }
            
            GameObjectContentNode contentNode = new GameObjectContentNode()
            {
                Token = context.Start,
                Statements = VisitStatementBlock(context.statementBlock()),
                TypeNode = gameObjectContentTypeNode,
            };


            return contentNode;
        }

        #endregion

        #region Expressions

        public ExpressionNode VisitExpression(DazelParser.ExpressionContext context)
        {
            return VisitSumExpression(context.sumExpression());
        }

        public ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context)
        {
            if (context.ChildCount > 1)
            {
                return new SumExpressionNode
                {
                    Token = context.Start,
                    Left = VisitSumExpression(context.sumExpression()),
                    OperationNode = VisitSumOperation(context.sumOperation()),
                    Right = VisitFactorExpression(context.factorExpression())
                };
            }

            return VisitFactorExpression(context.factorExpression());
        }

        public ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context)
        {
            if (context.ChildCount > 1)
            {
                return new FactorExpressionNode
                {
                    Token = context.Start,
                    Left = VisitFactorExpression(context.factorExpression()),
                    OperationNode = VisitFactorOperation(context.factorOperation()),
                    Right = VisitTerminalExpression(context.terminalExpression())
                };
            }

            return VisitTerminalExpression(context.terminalExpression());
        }

        public ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context)
        {
            if (context.expression() != null)
            {
                return VisitExpression(context.expression());
            }

            return VisitValue(context.value());
        }

        public ValueNode VisitValue(DazelParser.ValueContext context)
        {
            if (context.array() != null)
            {
                return VisitArray(context.array());
            }

            if (context.memberAccess() != null)
            {
                return VisitMemberAccess(context.memberAccess());
            }

            if (context.functionInvocation() != null)
            {
                return VisitFunctionInvocation(context.functionInvocation()).Function.ValueNode;
            }
            
            switch (context.terminalValue.Type)
            {
                case DazelLexer.IDENTIFIER:
                    return new IdentifierValueNode
                    {
                        Token = context.Start,
                        Identifier = context.GetText()
                    };
                case DazelLexer.INT:
                    return new IntValueNode
                    {
                        Token = context.Start,
                        Value = int.Parse(context.GetText())
                    };
                case DazelLexer.FLOAT:
                    return new FloatValueNode
                    {
                        Token = context.Start,
                        Value = float.Parse(context.GetText())
                    };
                case DazelLexer.STRING:
                    return new StringNode
                    {
                        Token = context.Start,
                        Value = context.GetText().Replace("\"", string.Empty)
                    };
                default:
                    throw new ArgumentException("Invalid value passed.");
            }
        }

        public ArrayNode VisitArray(DazelParser.ArrayContext context)
        {
            return new ArrayNode()
            {
                Token = context.Start,
                Values = VisitValueList(context.valueList())
            };
        }

        public List<ValueNode> VisitValueList(DazelParser.ValueListContext context)
        {
            List<ValueNode> values = new List<ValueNode>();
            
            if (context.value() == null) return values;
            
            values.Add(VisitValue(context.value()));

            if (context.valueList() != null)
            {
                values.AddRange(VisitValueList(context.valueList()));
            }

            return values;
        }

        public FactorOperationNode VisitFactorOperation(DazelParser.FactorOperationContext context)
        {
            char op = context.DIVISION_OP() != null ? Operators.DivOp : Operators.MultOp;
            
            return new FactorOperationNode()
            {
                Token = context.Start,
                Operator = op
            };
        }

        public SumOperationNode VisitSumOperation(DazelParser.SumOperationContext context)
        {
            char op = context.PLUS_OP() != null ? Operators.AddOp : Operators.MinOp;

            return new SumOperationNode()
            {
                Token = context.Start,
                Operator = op
            };
        }

        public MemberAccessNode VisitMemberAccess(DazelParser.MemberAccessContext context)
        {
            MemberAccessNode memberAccessNode;

            // X.Y
            if (context.children.Count == 3)
            {
                memberAccessNode = new MemberAccessNode
                {
                    Token = context.Start,
                    Identifiers =
                    {
                        context.GetChild(0).GetText(),
                        context.GetChild(2).GetText(),
                    }
                };
            }
            else if (context.children.Count == 5) // X.Y.Z
            {
                memberAccessNode = new MemberAccessNode
                {
                    Token = context.Start,
                    Identifiers =
                    {
                        context.GetChild(0).GetText(),
                        context.GetChild(2).GetText(),
                        context.GetChild(4).GetText(),
                    }
                };
            }
            else
            {
                throw new ArgumentException($"Member Access {context.GetChild(0).GetText()}");
            }
            
            return memberAccessNode;
        }

        #endregion

        #region Statements

        public List<StatementNode> VisitStatementList(DazelParser.StatementListContext context)
        {
            List<StatementNode> statements = new List<StatementNode>();
            
            if (context.statementBlock() != null)
            {
                statements.Add(new StatementBlockNode()
                {
                    Token = context.Start,
                    Statements = new List<StatementNode>(VisitStatementBlock(context.statementBlock()))
                });
            }
            
            if (context.statement() != null)
            {
                statements.Add(VisitStatement(context.statement()));
            }

            if (context.statementList() != null)
            {
                statements.AddRange(VisitStatementList(context.statementList()));
            }

            return statements;
        }

        public StatementNode VisitStatement(DazelParser.StatementContext context)
        {
            IParseTree child = context.GetChild(0);
            
            if (child.GetType() == typeof(DazelParser.IfStatementContext))
            {
                return VisitIfStatement(context.ifStatement());
            }

            if (child.GetType() == typeof(DazelParser.RepeatLoopContext))
            {
                return VisitRepeatLoop(context.repeatLoop());
            }

            if (child.GetType() == typeof(DazelParser.StatementExpressionContext))
            {
                return VisitStatementExpression(context.statementExpression());
            }

            throw new ArgumentException("Invalid statement");
        }

        public RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            return new RepeatNode()
            {
                Token = context.Start
            };
        }

        public IfStatementNode VisitIfStatement(DazelParser.IfStatementContext context)
        {
            return new IfStatementNode()
            {
                Token = context.Start
            };
        }

        public StatementExpressionNode VisitStatementExpression(DazelParser.StatementExpressionContext context)
        {
            if (context.assignment() != null)
            {
                return VisitAssignment(context.assignment());
            }
            
            FunctionInvocationNode functionInvocation = VisitFunctionInvocation(context.functionInvocation());

            return functionInvocation;
        }

        public FunctionInvocationNode VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            var functionInvocationNode = new FunctionInvocationNode()
            {
                Token = context.Start,
                Identifier = context.IDENTIFIER().GetText(),
                Parameters = VisitValueList(context.valueList()),
            };
            
            functionInvocationNode.Create();
            
            FunctionSymbolTableEntry entry = new FunctionSymbolTableEntry(functionInvocationNode.ReturnType, functionInvocationNode.Parameters);

            CurrentTopScope.AddOrUpdateSymbol(functionInvocationNode.Identifier, entry);
            
            return functionInvocationNode;
        }

        public StatementExpressionNode VisitAssignment(DazelParser.AssignmentContext context)
        {
            var assignmentNode = new AssignmentNode()
            {
                Token = context.Start,
                Identifier = context.IDENTIFIER().GetText(),
                Expression = VisitExpression(context.expression())
            };
            
            SymbolType expressionType = new ExpressionTypeChecker(CurrentTopScope).GetType(assignmentNode.Expression);
            ValueNode expressionValue;
            
            switch (expressionType)
            {
                case SymbolType.Null:
                    expressionValue = null;
                    break;
                case SymbolType.Float:
                    var floatEval = new ExpressionEvaluator<float>(new FloatCalculator(assignmentNode.Token));
                    assignmentNode.Expression.Accept(floatEval);
                    expressionValue = new FloatValueNode() {Value = floatEval.Result};
                    break;
                case SymbolType.String:
                    var stringEval = new ExpressionEvaluator<string>(new StringOperations(assignmentNode.Token));
                    assignmentNode.Expression.Accept(stringEval);
                    expressionValue = new StringNode() {Value = stringEval.Result};
                    break;
                case SymbolType.Integer:
                    var intEval = new ExpressionEvaluator<int>(new IntCalculator(assignmentNode.Token));
                    assignmentNode.Expression.Accept(intEval);
                    expressionValue = new FloatValueNode() {Value = intEval.Result};
                    break;
                case SymbolType.Array:
                    var arrayEval = new ExpressionEvaluator<ArrayNode>(new ArrayCalculator(assignmentNode.Token));
                    assignmentNode.Expression.Accept(arrayEval);
                    expressionValue = arrayEval.Result;
                    break;
                case SymbolType.Exit:
                    var exitEval = new ExpressionEvaluator<ExitValueNode>(new ExitValueCalculator(assignmentNode.Token));
                    assignmentNode.Expression.Accept(exitEval);
                    expressionValue = exitEval.Result;
                    break;
                case SymbolType.Identifier:
                    var idEval = new ExpressionEvaluator<ValueNode>(new NoOpCalculator<ValueNode>(assignmentNode.Token));
                    assignmentNode.Expression.Accept(idEval);
                    expressionValue = idEval.Result;
                    break;
                case SymbolType.MemberAccess:
                    var maEval = new ExpressionEvaluator<ValueNode>(new NoOpCalculator<ValueNode>(assignmentNode.Token));
                    assignmentNode.Expression.Accept(maEval);
                    expressionValue = maEval.Result;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            assignmentNode.Expression = expressionValue;

            VariableSymbolTableEntry entry = new VariableSymbolTableEntry(expressionValue, expressionType);
            CurrentTopScope.AddOrUpdateSymbol(assignmentNode.Identifier, entry);

            return assignmentNode;
        }

        public List<StatementNode> VisitStatementBlock(DazelParser.StatementBlockContext context)
        {
            OpenScope();

            if (context.statementList().statementBlock() != null)
            {
                return VisitStatementBlock(context.statementList().statementBlock());
            }
            
            CloseScope();

            return VisitStatementList(context.statementList());
        }

        #endregion
    }
}