using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.StandardLibrary.Functions;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class TypeChecker : EnvironmentStore, IGameObjectVisitor, IStatementVisitor
    {
        protected override string GameObjectIdentifier { get; set; }
        
        private readonly AbstractSyntaxTree ast;

        public TypeChecker(AbstractSyntaxTree ast)
        {
            this.ast = ast;
        }

        public void Visit(GameObjectNode gameObjectNode)
        {
            GameObjectIdentifier = gameObjectNode.Identifier;
            
            OpenScope();
            
            foreach (GameObjectContentNode gameObjectContent in gameObjectNode.Contents)
            {
                Visit(gameObjectContent);
            }

            CloseScope();
        }

        public void Visit(EntityNode entityNode)
        {
        }

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
            OpenScope();

            foreach (StatementNode statementNode in gameObjectContentNode.Statements)
            {
                statementNode.Accept(this);
            }

            CloseScope();
        }

        public void Visit(MovePatternNode movePatternNode)
        {
        }

        public void Visit(ScreenNode screenNode)
        {
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            OpenScope();
            
            foreach (StatementNode statement in statementBlockNode.Statements)
            {
                statement.Accept(this);
            }

            CloseScope();
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            functionInvocationNode.Function.CurrentSymbolTable = CurrentTopScope;
            FunctionSymbolTableEntry entry = new FunctionSymbolTableEntry(functionInvocationNode.ReturnType, functionInvocationNode.Parameters);
            
            CurrentTopScope.AddOrUpdateSymbol(functionInvocationNode.Identifier, entry);
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            SymbolType expressionType = new ExpressionTypeChecker(ast, CurrentTopScope).GetType(assignmentNode.Expression);
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
        }
    }
}