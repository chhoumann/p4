using Interpreter.Ast.Nodes.ExpressionNodes.Values;

namespace Interpreter
{
    internal class PlayerModel
    {
        public IntValue Health => new IntValue() {Value = 100};
    }
}