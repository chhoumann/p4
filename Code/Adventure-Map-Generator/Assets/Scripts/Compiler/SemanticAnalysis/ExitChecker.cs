using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;

namespace Dazel.Compiler.SemanticAnalysis
{
    public class ExitChecker : ICompleteVisitor
    {
        private AbstractSyntaxTree abstractSyntaxTree;

        public ExitChecker(AbstractSyntaxTree abstractSyntaxTree)
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
            throw new System.NotImplementedException();
        }

        public void Visit(EntityNode entityNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ScreenNode screenNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(MapTypeNode mapTypeNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(DataTypeNodeNode dataTypeNodeNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ExitsTypeNodeNode exitsTypeNodeNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(PatternTypeNode patternTypeNode)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void Visit(RepeatNode repeatNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            assignmentNode.Expression.Accept(this);
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FactorExpressionNode factorExpressionNode)
        {
            factorExpressionNode.Left.Accept(this);
            factorExpressionNode.Right.Accept(this);
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(SumExpressionNode sumExpressionNode)
        {
            sumExpressionNode.Left.Accept(this);
            sumExpressionNode.Right.Accept(this);
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
            terminalExpressionNode.Child.Accept(this);
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IntValueNode intValueNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ArrayNode arrayNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(StringNode stringNode)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            if (exitValueNode is TileExitValueNode tileExit)
            {
                if (!abstractSyntaxTree.TryRetrieveNode(tileExit.ToExit.Identifiers, out string identifier, out TileExitValueNode exitValue))
                {
                    throw new InvalidOperationException(
                        $"Exit {tileExit} is invalid: {string.Join(".", tileExit.ToExit.Identifiers)} does not exist.");
                }
            }

            if (exitValueNode is ScreenExitValueNode screenExit)
            {
                if (!abstractSyntaxTree.TryRetrieveGameObject(screenExit.ConnectedScreenIdentifier, out GameObjectNode go))
                {
                    throw new InvalidOperationException(
                        $"Exit {screenExit} is invalid: {screenExit.ConnectedScreenIdentifier} does not exist.");
                }
            }
        }
    }
}