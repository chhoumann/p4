using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;

namespace Interpreter
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            List<IParseTree> parseTrees = new();
            IEnumerable<string> files = SourceFileGetter.GetFilesInDirectory(@".\Antlr\");

            foreach (string file in files)
            {
                ICharStream stream = CharStreams.fromPath(file);
                ITokenSource lexer = new DazelLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                DazelParser parser = new(tokens) {BuildParseTree = true};
                
                parseTrees.Add(parser.start());
            }

            AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTrees);
        
            foreach (GameObject gameObject in ast.Root.GameObjects.Values)
            {
                AstPrinter astPrinter = new();
                astPrinter.Visit(gameObject);     
            }
           
            //Console.WriteLine(SymbolTable.Instance.Scopes[0].RetrieveSymbol("SomeVar"));

            // Front-end
            // Lexer -> Parser -> Build Parse Tree -> Transform to AST -> Static Semantic Analysis (Symbol Table)
            // Back-end
            // Interpretation hooked up to Dazel-Game API
        }
    }
}