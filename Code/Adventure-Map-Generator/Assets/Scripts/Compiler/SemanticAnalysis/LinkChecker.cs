using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.StandardLibrary.Functions;
using Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class LinkChecker : ICompleteVisitor
    {
        private readonly AbstractSyntaxTree abstractSyntaxTree;

        public LinkChecker(AbstractSyntaxTree abstractSyntaxTree)
        {
            this.abstractSyntaxTree = abstractSyntaxTree;
        }
        
        public void Visit(GameObjectNode gameObjectNode)
        {
            foreach (GameObjectContentNode gameObjectContentNode in gameObjectNode.Contents)
            {
                Visit(gameObjectContentNode);
            }
        }

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
            foreach (StatementNode statementNode in gameObjectContentNode.Statements)
            {
                statementNode.Accept(this);
            }
        }

        public void Visit(MovePatternNode movePatternNode)
        {
            
        }

        public void Visit(EntityNode entityNode)
        {
            
        }

        public void Visit(ScreenNode screenNode)
        {
            
        }

        public void Visit(MapTypeNode mapTypeNode)
        {
            
        }

        public void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode)
        {
            
        }

        public void Visit(DataTypeNodeNode dataTypeNodeNode)
        {
            
        }

        public void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode)
        {
            
        }

        public void Visit(ExitsTypeNodeNode exitsTypeNodeNode)
        {
            
        }

        public void Visit(PatternTypeNode patternTypeNode)
        {
            
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            foreach (StatementNode statementNode in statementBlockNode.Statements)
            {
                statementNode.Accept(this);
            }
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
            
        }

        public void Visit(RepeatNode repeatNode)
        {
            
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            assignmentNode.Expression?.Accept(this);
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            switch (functionInvocationNode.Function)
            {
                case ScreenExitFunction screenExitFunction:
                {
                    if (!abstractSyntaxTree.TryRetrieveGameObject(screenExitFunction.ConnectedScreenIdentifier))
                    {
                        DazelLogger.EmitError(
                            $"Screen {string.Join(".", screenExitFunction.ConnectedScreenIdentifier)} does not exist.", functionInvocationNode.Token);
                    }
                    break;
                }
                case ExitFunction exitFunction:
                    exitFunction.MemberAccessNode.Accept(this);
                    break;
                case PrintFunction printFunction:
                    printFunction.Log();
                    break;
            }

            foreach (ValueNode valueNode in functionInvocationNode.Parameters)
            {
                if (valueNode is MemberAccessNode memberAccessNode)
                {
                    memberAccessNode.Accept(this);
                }
            }
        }

        public void Visit(FactorExpressionNode factorExpressionNode)
        {
            factorExpressionNode.Left.Accept(this);
            factorExpressionNode.Right.Accept(this);
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
            
        }

        public void Visit(SumExpressionNode sumExpressionNode)
        {
            sumExpressionNode.Left.Accept(this);
            sumExpressionNode.Right.Accept(this);
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
            
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
            terminalExpressionNode.Child.Accept(this);
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
            var calc = new NoOpCalculator<NoOpCalculator<ValueNode>>(memberAccessNode.Token);
         
            new ExpressionEvaluatorLinkChecker<NoOpCalculator<ValueNode>>(calc).Visit(memberAccessNode);
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
        }

        public void Visit(IntValueNode intValueNode)
        {
            
        }

        public void Visit(ArrayNode arrayNode)
        {
            
        }

        public void Visit(StringNode stringNode)
        {
            
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            if (exitValueNode is TileExitValueNode tileExit)
            {
                EnvironmentStore.AccessMember(tileExit.ToExit);
            }

            if (exitValueNode is ScreenExitValueNode screenExit)
            {
                if (!abstractSyntaxTree.TryRetrieveGameObject(screenExit.ConnectedScreenIdentifier))
                {
                    DazelLogger.EmitError(
                        $"Exit {screenExit} is invalid: {screenExit.ConnectedScreenIdentifier} does not exist.", screenExit.Token);
                }
            }
        }
    }
}