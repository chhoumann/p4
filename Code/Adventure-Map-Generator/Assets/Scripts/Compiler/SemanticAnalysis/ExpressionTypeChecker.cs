using System;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Expressions;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Visitors;
using UnityEngine;

namespace Dazel.Compiler.SemanticAnalysis
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
                if (currentType == SymbolType.Null || currentType == SymbolType.Integer && (value == SymbolType.Float || value == SymbolType.Integer))
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
        
        public void Visit(SumExpressionNode sumExpressionNode)
        {
            if (sumExpressionNode.Left == null)
            {
                throw new InvalidOperationException("Expression left operand is null");
            }
            
            if (sumExpressionNode.Right == null)
            {
                throw new InvalidOperationException("Expression right operand is null");
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
            if (ast.TryRetrieveNode(memberAccessNode.Identifiers, out ValueNode value))
            {
                CurrentType = value.Type;
            }
            else
            {
                throw new InvalidOperationException($"{string.Join(".", memberAccessNode.Identifiers)} was not found.");
            }
        }

        public void Visit(FloatValueNode floatValueNode)
        {
            CurrentType = floatValueNode.Type;
        }

        public void Visit(IdentifierValueNode identifierValueNode)
        {
            CurrentType = symbolTable.RetrieveSymbol(identifierValueNode.Value).Type;
        }

        public void Visit(IntValueNode intValueNode)
        {
            CurrentType = intValueNode.Type;
        }

        public void Visit(ArrayNode arrayNode)
        {
            CurrentType = arrayNode.Type;

            SymbolType valueType = arrayNode.Values[0].Type;
            foreach (ValueNode value in arrayNode.Values)
            {
                if (value.Type == valueType)
                {
                    valueType = value.Type;
                }
                else
                {
                    throw new InvalidOperationException($"Type mismatch. {value} is not {currentType}");
                }
            }
        }

        public void Visit(StringNode stringNode)
        {
            CurrentType = stringNode.Type;
        }

        public void Visit(ExitValueNode exitValueNode)
        {
            
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