using System;
using System.Text;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;
using Interpreter.Ast.Nodes.StatementNodes;
using Array = Interpreter.Ast.Nodes.ExpressionNodes.Array;

namespace Interpreter.Ast
{
    class AstPrinter : IVisitor
    {
        private StringBuilder sb = new();
        private int indentCount = 0;

        private void Indent()
        {
            sb.Append(new string(' ', indentCount * 2));
        }
        public void Visit(Array array)
        {
            sb.Append('[');
            for (int i = 0; i < array.Values.Count; i++)
            {
                Value arrayValue = array.Values[i];
                arrayValue.Accept(this);
                if (i < array.Values.Count - 1)
                {
                    sb.Append(',');
                }
            }

            sb.Append(']');
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
            sb.Append($"{memberAccess.Left}.{memberAccess.Right}");
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

        public void Visit(EntityType entityType)
        {
            sb.Append("Entity");
        }

        public void Visit(GameObject gameObject)
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

            Console.WriteLine(sb);
        }

        public void Visit(GameObjectContent gameObjectContent)
        {
            gameObjectContent.Type.Accept(this);
            foreach (StatementNode statementNode in gameObjectContent.Statements)
            {
                statementNode.Accept(this);
            }
        }

        public void Visit(MovePatternType movePatternType)
        {
            sb.Append("MovePattern");
        }

        public void Visit(ScreenType gameObjectContent)
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

        public void Visit(StatementExpression statementExpression)
        {
            if (statementExpression is FunctionInvocation functionInvocation)
            {
                indentCount += 2;
                Indent();
                
                sb.Append(functionInvocation.Identifier);
                sb.Append('(');
                
                for (int i = 0; i < functionInvocation.Parameters.Count; i++)
                {
                    Value functionInvocationParameter = functionInvocation.Parameters[i];
                    functionInvocationParameter.Accept(this);
                    
                    if (i < functionInvocation.Parameters.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                
                sb.Append(')');
                indentCount -= 2;
            }

            if (statementExpression is AssignmentNode assignmentNode)
            {
                indentCount += 2;
                Indent();
                
                sb.Append($"{assignmentNode.Identifier} = ");
                assignmentNode.Expression.Accept(this);
                indentCount -= 2;
            }

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
    }
}