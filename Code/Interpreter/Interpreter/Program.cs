using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Interpreter.Ast;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.SemanticAnalysis;

namespace Interpreter
{
    public static class Program
    {
        private const string SourceFileDirectory = @".\Antlr\";
        
        private static void Main(string[] args)
        {
            List<IParseTree> parseTrees = new List<IParseTree>();
            IEnumerable<string> files = SourceFileGetter.GetFilesInDirectory(SourceFileDirectory);

            foreach (string file in files)
            {
                ICharStream stream = CharStreams.fromPath(file);
                ITokenSource lexer = new DazelLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                DazelParser parser = new DazelParser(tokens) {BuildParseTree = true};
                
                parseTrees.Add(parser.start());
            }

            AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTrees);
        
            foreach (GameObject gameObject in ast.Root.GameObjects.Values)
            {
                AstPrinter astPrinter = new AstPrinter();
                astPrinter.Visit(gameObject);     
            }
            
            foreach (GameObject gameObject in ast.Root.GameObjects.Values)
            {
                new TypeChecker(ast).Visit(gameObject);
            }
           
            //Console.WriteLine(SymbolTable.Instance.Scopes[0].RetrieveSymbol("SomeVar"));

            // Front-end
            // Lexer -> Parser -> Build Parse Tree -> Transform to AST -> Static Semantic Analysis (Symbol Table)
            // Back-end
            // Interpretation hooked up to Dazel-Game API
        }
    }
}