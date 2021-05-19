using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class TypeChecker : IGameObjectVisitor, IStatementVisitor
    {
        private readonly AbstractSyntaxTree ast;
        private string currentGameObject;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }

        public void Visit(GameObjectNode gameObjectNode)
        {
            currentGameObject = gameObjectNode.Identifier;
            foreach (GameObjectContentNode gameObjectContent in gameObjectNode.Contents)
            {
                Visit(gameObjectContent);
            }

        }

        public void Visit(EntityNode entityNode)
        {
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

        public void Visit(ScreenNode screenNode)
        {
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            foreach (StatementNode statement in statementBlockNode.Statements)
            {
                statement.Accept(this);
            }
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
        }

        public void Visit(AssignmentNode assignmentNode)
        {
        }
    }
}