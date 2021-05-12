using System;
using System.Collections.Generic;
using Dazel.IntermediateModels;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Interpreter.Ast.Nodes.StatementNodes;
using Dazel.Interpreter.Ast.Visitors;
using Dazel.Interpreter.StandardLibrary;
using Dazel.Interpreter.StandardLibrary.Functions.MapFunctions;
using UnityEngine;

namespace Dazel.Interpreter.CodeGeneration
{
    public interface ICodeGenerator<out TGameObject>
    {
        public TGameObject Generate();
    }
    
    public sealed class ScreenGenerator : ICodeGenerator<ScreenModel>, ICompleteVisitor
    {
        private readonly ScreenModel screenModel;
        private readonly List<GameObjectContentNode> contents;

        public ScreenGenerator(List<GameObjectContentNode> contents)
        {
            this.contents = contents;
            screenModel = new ScreenModel();
        }
        
        public ScreenModel Generate()
        {
            foreach (GameObjectContentNode content in contents)
            {
                content.Accept(this);
            }

            return screenModel;
        }

        public void Visit(GameObjectNode gameObjectNode)
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
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(AssignmentNode assignmentNode)
        {
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            switch (functionInvocationNode.Function)
            {
                case SizeFunction sizeFunction:
                    screenModel.Width = sizeFunction.Width;
                    screenModel.Height = sizeFunction.Height;
                    break;
            }
        }

        public void Visit(FactorExpressionNode factorExpressionNode)
        {
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
        }

        public void Visit(SumExpressionNode sumExpressionNode)
        {
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
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
        }
    }
    
    public sealed class CodeGenerator : ICompleteVisitor
    {
        public void Visit(GameObjectNode gameObjectNode)
        {
            
        }

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
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
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
        }

        public void Visit(RepeatNode repeatNode)
        {
        }

        public void Visit(AssignmentNode assignmentNode)
        {
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
        }

        public void Visit(FactorExpressionNode factorExpressionNode)
        {
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
        }

        public void Visit(SumExpressionNode sumExpressionNode)
        {
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
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
        }
    }
}