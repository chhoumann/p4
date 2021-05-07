using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;

namespace P4.MapGenerator.Interpreter.Ast
{
    public interface IAstBuilder
    {
        AbstractSyntaxTree BuildAst(IEnumerable<IParseTree> parseTrees);
        AbstractSyntaxTree BuildAst(IParseTree parseTrees);
        DGameObject VisitGameObject(DazelParser.GameObjectContext context);
        List<GameObjectContent> VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectContent VisitGameObjectContent(DazelParser.GameObjectContentContext context);
        ExpressionNode VisitExpression(DazelParser.ExpressionContext context);
        ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context);
        ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context);
        ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context);
        ValueNode VisitValue(DazelParser.ValueContext context);
        ArrayNode VisitArray(DazelParser.ArrayContext context);
        List<ValueNode> VisitValueList(DazelParser.ValueListContext context);
        FactorOperation VisitFactorOperation(DazelParser.FactorOperationContext context);
        SumOperation VisitSumOperation(DazelParser.SumOperationContext context);
        MemberAccess VisitMemberAccess(DazelParser.MemberAccessContext context);
        List<StatementNode> VisitStatementList(DazelParser.StatementListContext context);
        StatementNode VisitStatement(DazelParser.StatementContext context);
        RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context);
        IfStatement VisitIfStatement(DazelParser.IfStatementContext context);
        StatementExpression VisitStatementExpression(DazelParser.StatementExpressionContext context);
        FunctionInvocation VisitFunctionInvocation(DazelParser.FunctionInvocationContext context);
        StatementExpression VisitAssignment(DazelParser.AssignmentContext context);
        List<StatementNode> VisitStatementBlock(DazelParser.StatementBlockContext context);
    }
}