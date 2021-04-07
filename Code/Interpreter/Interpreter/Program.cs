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
            DazelParser parser = new(tokens) { BuildParseTree = true };

            IParseTree parseTree = parser.start();
            AbstractSyntaxTree ast = new(parseTree);
            
            AstPrinter astPrinter = new();
            astPrinter.Visit(ast.Root);
            
            // Front-end
            // Lexer -> Parser -> Build Parse Tree -> Transform to AST -> Static Semantic Analysis (Symbol Table)
            // Back-end
            // Interpretation hooked up to Dazel-Game API
        }
    }
}