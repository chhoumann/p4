using Interpreter.Ast.Nodes.ValueNodes;

namespace Interpreter.Ast.Visitors
{
    public interface IValueVisitor
    {
        void Visit(MemberAccess memberAccess);
        void Visit(FloatValue floatValue);
        void Visit(IdentifierValue identifierValue);
        void Visit(IntValue intValue);
        void Visit(ArrayNode arrayNode);
    }
}