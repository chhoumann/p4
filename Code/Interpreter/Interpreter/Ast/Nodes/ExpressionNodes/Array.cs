namespace Interpreter.Ast.Nodes.ExpressionNodes
{
    public sealed class Array : Value
    {
        public ValueList Values { get; set; }
    }
}