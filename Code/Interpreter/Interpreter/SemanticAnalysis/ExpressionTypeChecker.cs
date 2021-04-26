using System;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.ExpressionNodes.Expressions;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Visitors;
using Interpreter.SemanticAnalysis.API.Values;

namespace Interpreter.SemanticAnalysis
{
    public sealed class ExpressionTypeChecker : IExpressionVisitor
    {
        private readonly AbstractSyntaxTree ast;
        private readonly SymbolTable<SymbolTableEntry> symbolTable;

        public ExpressionTypeChecker(AbstractSyntaxTree ast, SymbolTable<SymbolTableEntry> symbolTable)
        {
            this.ast = ast;
            this.symbolTable = symbolTable;
        }
        
        private SymbolType currentType = SymbolType.Null;
        private SymbolType CurrentType
        {
            get => currentType;
            set
            {
                if (currentType == SymbolType.Null || currentType == SymbolType.Integer && (value is SymbolType.Float or SymbolType.Integer))
                {
                    currentType = value;
                    return;
                }
                
                throw new InvalidOperationException($"Type mismatch. {value} is not {currentType}");
            }
        }
        
        public SymbolType GetType(ExpressionNode expression)
        {
            expression.Accept(this);
            
            return CurrentType;
        }
        
        public void Visit(SumExpression sumExpression)
        {
            sumExpression.Left.Accept(this);
            sumExpression.Right.Accept(this);
        }
        
        public void Visit(FactorExpression factorExpression)
        {
            factorExpression.Left.Accept(this);
            factorExpression.Right.Accept(this);
        }

        public void Visit(TerminalExpression terminalExpression)
        {
            terminalExpression.Child.Accept(this);
        }
        
        public void Visit(MemberAccess memberAccess)
        {
            if (ast.TryRetrieveNode(memberAccess.Identifiers, out ValueNode value))
            {
                CurrentType = value.Type;
            }

            //CurrentType = symbolTable.RetrieveSymbol(memberAccess.Identifiers).Type;
        }

        public void Visit(FloatValue floatValue)
        {
            CurrentType = floatValue.Type = SymbolType.Float;
        }

        public void Visit(IdentifierValue identifierValue)
        {
            CurrentType = symbolTable.RetrieveSymbol(identifierValue.Value).Type;
        }

        public void Visit(IntValue intValue)
        {
            CurrentType = intValue.Type = SymbolType.Integer;
        }

        public void Visit(ArrayNode arrayNode)
        {
            foreach (ValueNode value in arrayNode.Values)
            {
                value.Accept(this);
            }

            foreach (ValueNode value in arrayNode.Values)
            {
                // Throws if one of the types are different - see CurrentType setter
                CurrentType = value.Type;
            }
        }

        public void Visit(ExitValue exitValue)
        {
            
        }

        #region IVisitor unimplemented
        public void Visit(SumOperation sumOperation)
        {
        }
        
        public void Visit(FactorOperation factorOperation)
        {
        }
        #endregion
    }
}