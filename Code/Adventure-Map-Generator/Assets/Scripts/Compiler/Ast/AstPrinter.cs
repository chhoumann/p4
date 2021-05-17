using System.Text;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using Dazel.Compiler.Ast.Visitors;
using UnityEngine;

namespace Dazel.Compiler.Ast
{
    public sealed class AstPrinter : ICompleteVisitor
    {
        private readonly StringBuilder sb = new StringBuilder();

        private int indentCount;

        private void Indent()
        {
            sb.Append(new string(' ', indentCount * 2));
        }
        
        public void Visit(ArrayNode arrayNode)
        {
            sb.Append('[');
            for (int i = 0; i < arrayNode.Values.Count; i++)
            {
                ValueNode arrayValue = arrayNode.Values[i];
                arrayValue.Accept(this);
                if (i < arrayNode.Values.Count - 1)
                {
                    sb.Append(',');
                }
            }

            sb.Append(']');
        }

        public void Visit(StringNode stringNode)
        {
            sb.Append($"\"{stringNode.Value}\"");
        }

        public void Visit(FactorExpressionNode factorExpressionNode)
        {
            factorExpressionNode.Left.Accept(this);
            factorExpressionNode.OperationNode.Accept(this);
            factorExpressionNode.Right.Accept(this);
        }

        public void Visit(FactorOperationNode factorOperationNode)
        {
            sb.Append(factorOperationNode.Operator);
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            sb.Append(floatValueNode.Value);
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            sb.Append(identifierValueNode.Value);
        }

        public void Visit(IntValueNode intValueNode)
        {
            sb.Append(intValueNode.Value);
        }

        public void Visit(MemberAccessNode memberAccessNode)
        {
            sb.Append(string.Join(".", memberAccessNode.Identifiers.ToArray()));
        }

        public void Visit(SumExpressionNode sumExpressionNode)
        {
            sumExpressionNode.Left.Accept(this);
            sumExpressionNode.OperationNode.Accept(this);
            sumExpressionNode.Right.Accept(this);
        }

        public void Visit(SumOperationNode sumOperationNode)
        {
            sb.Append(sumOperationNode.Operator);
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
            terminalExpressionNode.Child.Accept(this);
        }

        public void Visit(EntityNode entityNode)
        {
            sb.Append("Entity");
        }

        public void Visit(GameObjectNode gameObjectNode)
        {
            gameObjectNode.TypeNode.Accept(this);
            sb.Append(" ");
            sb.AppendLine(gameObjectNode.Identifier);
            foreach (GameObjectContentNode gameObjectContent in gameObjectNode.Contents)
            {
                indentCount += 2;
                Indent();
                gameObjectContent.Accept(this);
                indentCount -= 2;
            }

            Debug.Log(sb);
        }

        public void Visit(GameObjectContentNode gameObjectContentNode)
        {
            gameObjectContentNode.TypeNode.Accept(this);
            
            foreach (StatementNode statementNode in gameObjectContentNode.Statements)
            {
                statementNode.Accept(this);
            }
        }

        public void Visit(MovePatternNode movePatternNode)
        {
            sb.Append("MovePattern");
        }

        public void Visit(ScreenNode screenNode)
        {
            sb.Append("Screen");
        }

        public void Visit(IfStatementNode ifStatementNode)
        {
            sb.AppendLine("if - not implemented");
        }

        public void Visit(RepeatNode repeatNode)
        {
            sb.AppendLine("repeat - not implemented");
        }

        public void Visit(FunctionInvocationNode functionInvocationNode)
        {
            indentCount += 2;
            Indent();
                
            sb.Append(functionInvocationNode.Identifier);
            sb.Append('(');
                
            for (int i = 0; i < functionInvocationNode.Parameters.Count; i++)
            {
                ValueNode functionInvocationParameter = functionInvocationNode.Parameters[i];
                functionInvocationParameter.Accept(this);
                    
                if (i < functionInvocationNode.Parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }
                
            sb.Append(')');
            indentCount -= 2;
            sb.AppendLine("");
        }

        public void Visit(AssignmentNode assignmentNode)
        {
            indentCount += 2;
            Indent();
                
            sb.Append($"{assignmentNode.Identifier} = ");
            assignmentNode.Expression?.Accept(this);
            indentCount -= 2;
            
            sb.AppendLine("");
        }

        public void Visit(DataTypeNodeNode dataTypeNodeNode)
        {
            sb.AppendLine("Data");
        }

        public void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode)
        {
            sb.AppendLine("Entities");
        }

        public void Visit(ExitsTypeNodeNode exitsTypeNodeNode)
        {
            sb.AppendLine("Exit");
        }

        public void Visit(MapTypeNode mapTypeNode)
        {
            sb.AppendLine("Map");
        }

        public void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode)
        {
            sb.AppendLine("OnScreenEntered");
        }

        public void Visit(PatternTypeNode patternTypeNode)
        {
            sb.AppendLine("Pattern");
        }

        public void Visit(StatementBlockNode statementBlockNode)
        {
            indentCount += 2;
            Indent();
            sb.AppendLine("{");
            
            foreach (StatementNode statementBlockStatement in statementBlockNode.Statements)
            {
                statementBlockStatement.Accept(this);
            }
            
            Indent();
            sb.AppendLine("}");
            indentCount -= 2;
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            sb.Append(exitValueNode);
        }
    }
}