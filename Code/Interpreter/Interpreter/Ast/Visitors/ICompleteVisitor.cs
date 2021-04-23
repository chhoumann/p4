namespace Interpreter.Ast.Visitors
{
    public interface ICompleteVisitor : IExpressionVisitor, IGameObjectVisitor, IGameObjectContentTypeVisitor, IStatementVisitor
    {
        
    }
}