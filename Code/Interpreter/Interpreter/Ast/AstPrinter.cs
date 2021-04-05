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
            sb.AppendLine($"{memberAccess.Left}.{memberAccess.Right}");
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
            sb.AppendLine("Entity Type (not implemented)");
        }

        public void Visit(GameObject gameObject)
        {
            gameObject.Type.Accept(this);
            sb.AppendLine(gameObject.Identifier);
            foreach (GameObjectContent gameObjectContent in gameObject.Contents)
            {
                gameObjectContent.Accept(this);
            }
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
            sb.AppendLine("MovePattern");
        }

        public void Visit(ScreenType gameObjectContent)
        {
            sb.AppendLine("Screen");
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
            }

            if (statementExpression is AssignmentNode assignmentNode)
            {
                sb.Append($"{assignmentNode.Identifier} = ");
                assignmentNode.Expression.Accept(this);
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

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}