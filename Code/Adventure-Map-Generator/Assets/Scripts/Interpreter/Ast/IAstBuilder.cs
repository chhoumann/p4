using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.Ast.Nodes.StatementNodes;

namespace Dazel.Interpreter.Ast
{
    public interface IAstBuilder
    {
        AbstractSyntaxTree BuildAst(IEnumerable<IParseTree> parseTrees);
        AbstractSyntaxTree BuildAst(IParseTree parseTrees);
        GameObjectNode VisitGameObject(DazelParser.GameObjectContext context);
        List<GameObjectContentNode> VisitGameObjectContents(DazelParser.GameObjectContentsContext context);
        GameObjectContentNode VisitGameObjectContent(DazelParser.GameObjectContentContext context);
        ExpressionNode VisitExpression(DazelParser.ExpressionContext context);
        ExpressionNode VisitSumExpression(DazelParser.SumExpressionContext context);
        ExpressionNode VisitFactorExpression(DazelParser.FactorExpressionContext context);
        ExpressionNode VisitTerminalExpression(DazelParser.TerminalExpressionContext context);
        ValueNode VisitValue(DazelParser.ValueContext context);
        ArrayNode VisitArray(DazelParser.ArrayContext context);
        List<ValueNode> VisitValueList(DazelParser.ValueListContext context);
        FactorOperationNode VisitFactorOperation(DazelParser.FactorOperationContext context);
        SumOperationNode VisitSumOperation(DazelParser.SumOperationContext context);
        MemberAccessNode VisitMemberAccess(DazelParser.MemberAccessContext context);
        List<StatementNode> VisitStatementList(DazelParser.StatementListContext context);
        StatementNode VisitStatement(DazelParser.StatementContext context);
        RepeatNode VisitRepeatLoop(DazelParser.RepeatLoopContext context);
        IfStatementNode VisitIfStatement(DazelParser.IfStatementContext context);
        StatementExpressionNode VisitStatementExpression(DazelParser.StatementExpressionContext context);
        FunctionInvocationNode VisitFunctionInvocation(DazelParser.FunctionInvocationContext context);
        StatementExpressionNode VisitAssignment(DazelParser.AssignmentContext context);
        List<StatementNode> VisitStatementBlock(DazelParser.StatementBlockContext context);
    }
}