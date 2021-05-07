using System;
using System.Text;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;
using P4.MapGenerator.Interpreter.Ast.Visitors;
using UnityEngine;
using Screen = P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes.Screen;

namespace P4.MapGenerator.Interpreter.Ast
{
    internal sealed class AstPrinter : ICompleteVisitor
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

        public void Visit(FactorExpression factorExpression)
        {
            factorExpression.Left.Accept(this);
            factorExpression.Operation.Accept(this);
            factorExpression.Right.Accept(this);
        }

        public void Visit(FactorOperation factorOperation)
        {
            sb.Append(factorOperation.Operation);
        }

        public void Visit(FloatValue floatValue)
        {
            sb.Append(floatValue.Value);
        }

        public void Visit(IdentifierValue identifierValue)
        {
            sb.Append(identifierValue.Value);
        }

        public void Visit(IntValue intValue)
        {
            sb.Append(intValue.Value);
        }

        public void Visit(MemberAccess memberAccess)
        {
            sb.Append(string.Join(".", memberAccess.Identifiers.ToArray()));
        }

        public void Visit(SumExpression sumExpression)
        {
            sumExpression.Left.Accept(this);
            sumExpression.Operation.Accept(this);
            sumExpression.Right.Accept(this);
        }

        public void Visit(SumOperation sumOperation)
        {
            sb.Append(sumOperation.Operation);
        }

        public void Visit(TerminalExpression terminalExpression)
        {
            terminalExpression.Child.Accept(this);
        }

        public void Visit(Entity entity)
        {
            sb.Append("Entity");
        }

        public void Visit(DGameObject gameObject)
        {
            gameObject.Type.Accept(this);
            sb.Append(" ");
            sb.AppendLine(gameObject.Identifier);
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                indentCount += 2;
                Indent();
                gameObjectContent.Accept(this);
                indentCount -= 2;
            }

            Debug.Log(sb);
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            gameObjectContent.Type.Accept(this);
            
            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
            }
        }

        public void Visit(MovePattern movePattern)
        {
            sb.Append("MovePattern");
        }

        public void Visit(Screen screen)
        {
            sb.Append("Screen");
        }

        public void Visit(IfStatement ifStatement)
        {
            sb.AppendLine("if - not implemented");
        }

        public void Visit(RepeatNode repeatNode)
        {
            sb.AppendLine("repeat - not implemented");
        }

        public void Visit(FunctionInvocation functionInvocation)
        {
            indentCount += 2;
            Indent();
                
            sb.Append(functionInvocation.Identifier);
            sb.Append('(');
                
            for (int i = 0; i < functionInvocation.Parameters.Count; i++)
            {
                ValueNode functionInvocationParameter = functionInvocation.Parameters[i];
                functionInvocationParameter.Accept(this);
                    
                if (i < functionInvocation.Parameters.Count - 1)
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

        public void Visit(DataType dataType)
        {
            sb.AppendLine("Data");
        }

        public void Visit(EntitiesType entitiesType)
        {
            sb.AppendLine("Entities");
        }

        public void Visit(ExitsType exitsType)
        {
            sb.AppendLine("Exit");
        }

        public void Visit(MapType mapType)
        {
            sb.AppendLine("Map");
        }

        public void Visit(OnScreenEnteredType onScreenEnteredType)
        {
            sb.AppendLine("OnScreenEntered");
        }

        public void Visit(PatternType patternType)
        {
            sb.AppendLine("Pattern");
        }

        public void Visit(StatementBlock statementBlock)
        {
            indentCount += 2;
            Indent();
            sb.AppendLine("{");
            
            foreach (StatementNode statementBlockStatement in statementBlock.Statements)
            {
                statementBlockStatement.Accept(this);
            }
            
            Indent();
            sb.AppendLine("}");
            indentCount -= 2;
        }

        public void Visit(ExitValue exitValue)
        {
            sb.Append(exitValue);
        }
    }
}