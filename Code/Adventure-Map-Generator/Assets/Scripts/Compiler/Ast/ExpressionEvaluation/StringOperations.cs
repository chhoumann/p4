﻿using Antlr4.Runtime;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.ErrorHandler;

namespace Dazel.Compiler.Ast.ExpressionEvaluation
{
    public sealed class StringOperations : Calculator<string>
    {
        public StringOperations(IToken token) : base(token) { }
        
        public override string Add(string a, string b) => string.Concat(a, b);

        public override string Subtract(string a, string b)
        {
            DazelLogger.EmitError($"Cannot subtract string {a} from {b}.", Token);
            
            return null;
        }

        public override string Multiply(string a, string b)
        {
            DazelLogger.EmitError($"Cannot multiply string {a} and {b}.", Token);
            
            return null;
        }

        public override string Divide(string a, string b)
        {
            DazelLogger.EmitError($"Cannot divide string {a} and {b}.", Token);
            
            return null;
        }

        public override string GetValue(int a) => a.ToString();

        public override string GetValue(float a) => a.ToString();

        public override string GetValue(string a) => a;

        public override string GetValue(ArrayNode a)
        {
            DazelLogger.EmitError("Arrays cannot be used as strings.", Token);
            
            return null;
        }

        public override string GetValue(ExitValueNode a)
        {
            DazelLogger.EmitError("Exits cannot be used as strings.", Token);
            
            return null;
        }
    }
}