﻿using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.StandardLibrary.Functions.EntitiesFunctions;
using Dazel.Compiler.StandardLibrary.Functions.ExitsFunctions;
using Dazel.Compiler.StandardLibrary.Functions.MapFunctions;
using Dazel.IntermediateModels;

namespace Dazel.Compiler.CodeGeneration
{
    public sealed class ScreenGenerator : ICodeGenerator<ScreenModel>, ICompleteVisitor
    {
        private readonly ScreenModel screenModel;
        private readonly List<GameObjectContentNode> contents;

        public ScreenGenerator(GameObjectNode gameObjectNode)
        {
            contents = gameObjectNode.Contents;
            screenModel = new ScreenModel(gameObjectNode.Identifier);
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
            assignmentNode.Expression.Accept(this);
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            switch (functionInvocationNode.Function)
            {
                case SizeFunction sizeFunction:
                    screenModel.Width = sizeFunction.Width;
                    screenModel.Height = sizeFunction.Height;
                    break;
                case FloorFunction floorFunction:
                    screenModel.TileStack.Push(new Floor(screenModel, floorFunction.TileName));
                    break;
                case SpawnEntityFunction spawnEntityFunction:
                    screenModel.Entities.Add(new EntityModel(spawnEntityFunction.EntityIdentifier, spawnEntityFunction.SpawnPosition));
                    break;
                case ScreenExitFunction screenExitFunction:
                    screenModel.ScreenExits.Add(new ScreenExitModel(screenExitFunction.ConnectedScreenIdentifier, screenExitFunction.ExitDirection));
                    break;
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
                DazelLogger.EmitWarning("TileExits have not been implemented yet.", tileExit.Token);
            }

            if (exitValueNode is ScreenExitValueNode screenExit)
            {
                screenModel.ScreenExits.Add(new ScreenExitModel(screenExit.ConnectedScreenIdentifier, screenExit.ExitDirection));
            }
        }
    }
}