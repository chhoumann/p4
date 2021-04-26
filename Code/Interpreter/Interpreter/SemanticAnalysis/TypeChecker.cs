﻿using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Interpreter.Ast.Visitors;

namespace Interpreter.SemanticAnalysis
{
    public sealed class TypeChecker : SemanticAnalysis, IGameObjectVisitor, IStatementVisitor
    {
        private readonly AbstractSyntaxTree ast;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }
        
        public void Visit(GameObject gameObject)
        {
            OpenScope();
            
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                Visit(gameObjectContent);
            }

            CloseScope();
        }

        public void Visit(Entity entity)
        {
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            OpenScope();

            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
            }

            CloseScope();
        }

        public void Visit(StatementBlock statementBlock)
        {
            OpenScope();
            
            foreach (StatementNode statement in statementBlock.Statements)
            {
                statement.Accept(this);
            }

            CloseScope();
        }
        
        public void Visit(MovePattern movePattern)
        {
        }

        public void Visit(Screen screen)
        {
        }

        public void Visit(IfStatement ifStatement) { }

        public void Visit(RepeatNode repeatNode) { }

        public void Visit(FunctionInvocation functionInvocation)
        {
            FunctionSymbolTableEntry entry = new(functionInvocation.ReturnType, functionInvocation.Parameters);
            
            EnvironmentStack.Peek().AddOrUpdateSymbol(functionInvocation.Identifier, entry);
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            SymbolTable<SymbolTableEntry> currentSymbolTable = EnvironmentStack.Peek();
            SymbolType expressionType = new ExpressionTypeChecker(ast, currentSymbolTable).GetType(assignmentNode.Expression);

            VariableSymbolTableEntry entry = new(expressionType);
            currentSymbolTable.AddOrUpdateSymbol(assignmentNode.Identifier, entry);
        }
    }
}