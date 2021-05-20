using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.ExpressionEvaluation;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.SemanticAnalysis
{
    public sealed class ExpressionTypeChecker : IExpressionVisitor
    {
        private readonly SymbolTable symbolTable;
        private readonly TypeHandler typeHandler;

        public ExpressionTypeChecker(SymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;
            typeHandler = new TypeHandler();
        }
        
        public SymbolType GetType(ExpressionNode expression)
        {
            if (expression == null)
                return SymbolType.Null;
            
            expression.Accept(this);
            
            return typeHandler.CurrentType;
        }
        
        public void Visit(SumExpressionNode sumExpressionNode)
        {
            if (sumExpressionNode.Left == null)
            {
                DazelLogger.EmitError("Expression left operand is null", sumExpressionNode.Token);
            }
            
            if (sumExpressionNode.Right == null)
            {
                DazelLogger.EmitError("Expression right operand is null", sumExpressionNode.Token);
            }
            
            sumExpressionNode.Left.Accept(this);
            sumExpressionNode.Right.Accept(this);
        }
        
        public void Visit(FactorExpressionNode factorExpressionNode)
        {
            factorExpressionNode.Left.Accept(this);
            factorExpressionNode.Right.Accept(this);
        }

        public void Visit(TerminalExpressionNode terminalExpressionNode)
        {
            terminalExpressionNode.Child.Accept(this);
        }
        
        public void Visit(MemberAccessNode memberAccessNode)
        {
            //ValueNode member = EnvironmentStore.AccessMember(memberAccessNode).ValueNode;
            //typeHandler.SetType(member.Type, member.Token);
            typeHandler.SetType(memberAccessNode.Type, memberAccessNode.Token);
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            typeHandler.SetType(floatValueNode.Type, floatValueNode.Token);
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            SymbolTableEntry entry = symbolTable.RetrieveSymbolInParentScope(identifierValueNode.Identifier);

            if (entry is VariableSymbolTableEntry variableSymbolTableEntry)
            {
                ExpressionNode expression = variableSymbolTableEntry.ValueNode;
                SetNumericalExpression(identifierValueNode, expression, entry.Type);
            }

            SymbolTableEntry symbolTableEntry = symbolTable.RetrieveSymbolInParentScope(identifierValueNode.Identifier);
            typeHandler.SetType(symbolTableEntry.Type, identifierValueNode.Token);
        }

        public void Visit(IntValueNode intValueNode)
        {
            typeHandler.SetType(intValueNode.Type, intValueNode.Token);
        }

        public void Visit(ArrayNode arrayNode)
        {
            typeHandler.SetType(arrayNode.Type, arrayNode.Token);
            
            SymbolType valueType = arrayNode.Values[0].Type;
            
            foreach (ValueNode value in arrayNode.Values)
            {
                if (value.Type == valueType)
                {
                    valueType = value.Type;
                }
                else
                {
                    DazelLogger.EmitError($"Type mismatch. {value} is not {typeHandler.CurrentType}", value.Token);
                }
            }
        }

        public void Visit(StringNode stringNode)
        {
            typeHandler.SetType(stringNode.Type, stringNode.Token); 
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            typeHandler.SetType(exitValueNode.Type, exitValueNode.Token); 
        }
        
        private void SetNumericalExpression(IdentifierValueNode identifierValueNode,
            ExpressionNode expressionNode, SymbolType expressionType)
        {
            switch (expressionType)
            {
                case SymbolType.Float:
                {
                    var floatValueNode = new FloatValueNode();
                    var expressionEvaluator = new ExpressionEvaluator<float>(new FloatCalculator(expressionNode.Token));

                    expressionNode.Accept(expressionEvaluator);

                    floatValueNode.Value = expressionEvaluator.Result;
                    identifierValueNode.ValueNode = floatValueNode;
                    break;
                }
                case SymbolType.Integer:
                {
                    var intValueNode = new IntValueNode();
                    var expressionEvaluator = new ExpressionEvaluator<int>(new IntCalculator(expressionNode.Token));

                    expressionNode.Accept(expressionEvaluator);

                    intValueNode.Value = expressionEvaluator.Result;
                    identifierValueNode.ValueNode = intValueNode;
                    break;
                }
            }
        }

        #region IVisitor unimplemented
        public void Visit(SumOperationNode sumOperationNode)
        {
        }
        
        public void Visit(FactorOperationNode factorOperationNode)
        {
        }
        #endregion
    }
}