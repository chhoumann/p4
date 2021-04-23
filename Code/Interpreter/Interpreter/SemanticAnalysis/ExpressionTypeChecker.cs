using System;
using Interpreter.Ast.Nodes.ExpressionNodes;
using Interpreter.Ast.Nodes.ValueNodes;
using Interpreter.Ast.Visitors;

namespace Interpreter.SemanticAnalysis
{
    public sealed class ExpressionTypeChecker : IValueVisitor, IExpressionVisitor
    {
        private readonly SymbolTable<SymbolTableEntry> symbolTable;

        public ExpressionTypeChecker(SymbolTable<SymbolTableEntry> symbolTable)
        {
            this.symbolTable = symbolTable;
        }
        
        private SymbolType currentType = SymbolType.Void;
        private SymbolType CurrentType
        {
            get => currentType;
            set
            {
                if (currentType == SymbolType.Void || currentType == SymbolType.Integer && value == SymbolType.Float)
                {
                    currentType = value;
                    return;
                }
                
                throw new InvalidOperationException("Type mismatch.");
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
            CurrentType = symbolTable.RetrieveSymbol(memberAccess.Identifiers).Type;
        }

        public void Visit(FloatValue floatValue)
        {
            CurrentType = SymbolType.Float;
        }

        public void Visit(IdentifierValue identifierValue)
        {
            CurrentType = symbolTable.RetrieveSymbol(identifierValue.Value).Type;
        }

        public void Visit(IntValue intValue)
        {
            CurrentType = SymbolType.Integer;
        }

        public void Visit(ArrayNode arrayNode)
        {
            foreach (ValueNode value in arrayNode.Values)
            {
                value.Accept(this);
            }
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