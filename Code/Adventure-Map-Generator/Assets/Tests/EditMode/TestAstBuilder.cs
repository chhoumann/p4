using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast;
using Dazel.Compiler.ErrorHandler;

namespace Tests.EditMode
{
    public static class TestAstBuilder
    {
        public static (AstBuilder, List<IParseTree> parseTrees) GetAstBuilderAndParseTrees(params string[] code)
        {
            List<IParseTree> parseTrees = new List<IParseTree>();
            DazelLogger.ThrowExceptions = true;

            foreach (string s in code)
            {
                ICharStream stream = CharStreams.fromString(s);
                ITokenSource lexer = new DazelLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                DazelParser parser = new DazelParser(tokens) {BuildParseTree = true};
                parser.AddErrorListener(new DazelErrorListener());

                parseTrees.Add(parser.start());
            }

            return (new AstBuilder(), parseTrees);
        }
        
        public static AbstractSyntaxTree BuildAst(params string[] code)
        {
            (AstBuilder astBuilder, List<IParseTree> parseTrees) = GetAstBuilderAndParseTrees(code);

            return astBuilder.BuildAst(parseTrees);
        }
    }
}