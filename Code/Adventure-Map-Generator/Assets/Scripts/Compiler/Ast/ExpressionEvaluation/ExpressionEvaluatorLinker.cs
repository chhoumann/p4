using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.SemanticAnalysis;
using UnityEngine;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class ExpressionEvaluatorLinker<T> : ExpressionEvaluator<T>
    {
        public ExpressionEvaluatorLinker(Calculator<T> calculator) : base(calculator) { }

        public override void Visit(MemberAccessNode memberAccessNode)
        {
            ValueNode member = EnvironmentStore.AccessMember(memberAccessNode).ValueNode;

            switch (member)
            {
                case ArrayNode arrayNode:
                    Result = Calculator.GetValue(arrayNode);
                    break;
                case IntValueNode intValueNode:
                    Result = Calculator.GetValue(intValueNode.Value);
                    break;
                case StringNode stringNode:
                    Result = Calculator.GetValue(stringNode.Value);
                    break;
                case FloatValueNode floatValueNode:
                    Result = Calculator.GetValue(floatValueNode.Value);
                    break;
            }
        }
    }
}