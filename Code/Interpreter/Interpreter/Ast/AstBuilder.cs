using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Interpreter.Ast.Nodes;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    internal sealed class AstBuilder : IAstBuilder
    {
        public AbstractSyntaxTree BuildAst(IEnumerable<IParseTree> parseTrees)
        {
            Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
            
            foreach (IParseTree parseTree in parseTrees)
            {
                DazelParser.GameObjectContext gameObjectContext = parseTree.GetChild(0) as DazelParser.GameObjectContext;
                GameObject gameObject = VisitGameObject(gameObjectContext);
                
                gameObjects.Add(gameObject.Identifier, gameObject);
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
        public GameObject VisitGameObject(DazelParser.GameObjectContext context)
        {
            GameObjectType type;
            
            switch (context.gameObjectType.Type)
            {
                case DazelLexer.SCREEN:
                    type = new Screen();
                    break;
                case DazelLexer.ENTITY:
                    type = new Entity();
                    break;
                case DazelLexer.MOVE_PATTERN:
                    type = new MovePattern();
                    break;
                default:
                    throw new ArgumentException("Type is not a GameObjectType!");
            }

            GameObject gameObject = new GameObject()
            {
                Identifier = context.GetChild(1).GetText(),
                Type = type,
                Contents = VisitGameObjectContents(context.gameObjectBlock().gameObjectContents())
            };
            
            return gameObject;
        }

        public List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context)
        {
            List<GameObjectContent> contents = new List<GameObjectContent>();

            if (context.gameObjectContent() == null) return contents;

            contents.Add(VisitGameObjectContent(context.gameObjectContent()));
            
            if (context.gameObjectContents() != null)
            {
                contents.AddRange(VisitGameObjectContents(context.gameObjectContents()));
            }

            return contents;
        }

        public GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context)
        {
            GameObjectContentType gameObjectContentType;
            
            switch (context.gameObjectContentType.Type)
            {
                case DazelLexer.MAP:
                    gameObjectContentType = new MapType();
                    break;
                case DazelLexer.ONSCREENENTERED:
                    gameObjectContentType = new OnScreenEnteredType();
                    break;
                case DazelLexer.ENTITIES:
                    gameObjectContentType = new EntitiesType();
                    break;
                case DazelLexer.EXITS:
                    gameObjectContentType = new ExitsType();
                    break;
                case DazelLexer.DATA:
                    gameObjectContentType = new DataType();
                    break;
                case DazelLexer.PATTERN:
                    gameObjectContentType = new PatternType();
                    break;
                default:
                    throw new ArgumentException("Invalid content type.");
            }
            
            GameObjectContent content = new GameObjectContent()
            {
                Statements = VisitStatementBlock(context.statementBlock()),
                Type = gameObjectContentType,
            };

            return content;
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
                return new SumExpression
                {
                    Left = VisitSumExpression(context.sumExpression()),
                    Operation = VisitSumOperation(context.sumOperation()),
                    Right = VisitFactorExpression(context.factorExpression())
                };
            }

            return VisitFactorExpression(context.factorExpression());
        }
        
        public ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context)
        {
            if (context.ChildCount > 1)
            {
                return new FactorExpression
                {
                    Left = VisitFactorExpression(context.factorExpression()),
                    Operation = VisitFactorOperation(context.factorOperation()),
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
                return VisitFunctionInvocation(context.functionInvocation()).Create();
            }

            switch (context.terminalValue.Type)
            {
                case DazelLexer.IDENTIFIER:
                    return new IdentifierValue
                    {
                        Value = context.GetText()
                    };
                case DazelLexer.INT:
                    return new IntValue
                    {
                        Value = int.Parse(context.GetText())
                    };
                case DazelLexer.FLOAT:
                    return new FloatValue
                    {
                        Value = float.Parse(context.GetText())
                    };
                case DazelLexer.STRING:
                    return new StringNode
                    {
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

        public FactorOperation VisitFactorOperation(DazelParser.FactorOperationContext context)
        {
            char op = context.DIVISION_OP() != null ? Operators.DivOp : Operators.MultOp;
            
            return new FactorOperation()
            {
                Operation = op
            };
        }
        
        public SumOperation VisitSumOperation(DazelParser.SumOperationContext context)
        {
            char op = context.PLUS_OP() != null ? Operators.AddOp : Operators.MinOp;

            return new SumOperation()
            {
                Operation = op
            };
        }
        
        public MemberAccess VisitMemberAccess(DazelParser.MemberAccessContext context)
        {
            MemberAccess memberAccess = new MemberAccess()
            {
                Identifiers =
                {
                    context.GetChild(0).GetText(),
                    context.GetChild(2).GetText(),
                    context.GetChild(4).GetText(),
                }
            };
            
            return memberAccess;
        }
        #endregion

        #region Statements
        public List<StatementNode> VisitStatementList(DazelParser.StatementListContext context)
        {
            List<StatementNode> statements = new List<StatementNode>();
            
            if (context.statementBlock() != null)
            {
                statements.Add(new StatementBlock()
                {
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
        
        // TOOD: Undecided
        public RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context)
        {
            // RepeatNode statements = VisitStatementList(context.statementList());

            return new RepeatNode();
        }

        // TOOD: Undecided
        public IfStatement VisitIfStatement(DazelParser.IfStatementContext context)
        {
            // // TODO: update expression visitor
            // var expression = new ExpressionVisitor().VisitExpression(context.expression());
            // var statements = VisitStatementList(context.statementList());

            return new IfStatement();
        }

        public StatementExpression VisitStatementExpression(DazelParser.StatementExpressionContext context)
        {
            if (context.assignment() != null)
            {
                return VisitAssignment(context.assignment());
            }

            return VisitFunctionInvocation(context.functionInvocation());
        }

        public FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context)
        {
            return new FunctionInvocation()
            {
                Identifier = context.IDENTIFIER().GetText(),
                Parameters = VisitValueList(context.valueList()),
            };
        }

        public StatementExpression VisitAssignment(DazelParser.AssignmentContext context)
        {
            return new AssignmentNode()
            {
                Identifier = context.IDENTIFIER().GetText(),
                Expression = VisitExpression(context.expression())
            };
        }

        public List<StatementNode> VisitStatementBlock(DazelParser.StatementBlockContext context)
        {
            if (context.statementList().statementBlock() != null)
            {
                return VisitStatementBlock(context.statementList().statementBlock());
            }
            
            return VisitStatementList(context.statementList());
        }
        #endregion
    }
}