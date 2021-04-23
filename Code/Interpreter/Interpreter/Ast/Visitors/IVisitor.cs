namespace Interpreter.Ast.Visitors
{
    public interface IVisitor : IExpressionVisitor, IGameObjectVisitor, IGameObjectContentTypeVisitor, IStatementVisitor
    {
        
    }
}