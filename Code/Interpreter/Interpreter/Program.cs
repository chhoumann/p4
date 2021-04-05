using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;

namespace Interpreter
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            ICharStream stream = CharStreams.fromPath(@".\Antlr\example.txt");
            ITokenSource lexer = new DazelLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            DazelParser parser = new DazelParser(tokens);

            parser.BuildParseTree = true;
            IParseTree tree = parser.start();
            AbstractSyntaxTree ast = new(tree);
        }
    }
}