namespace Interpreter.Ast.Visitors
{
    internal interface ICompleteVisitor : IExpressionVisitor, IGameObjectVisitor, IGameObjectContentTypeVisitor, IStatementVisitor
    {
        
    }
}