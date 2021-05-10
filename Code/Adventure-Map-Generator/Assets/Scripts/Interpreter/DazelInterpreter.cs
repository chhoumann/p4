using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.SemanticAnalysis;

namespace Dazel.Interpreter
{
    public sealed class DazelInterpreter
    {
        private readonly string sourceFileDirectory;

        public DazelInterpreter(string sourceFileDirectory)
        {
            this.sourceFileDirectory = sourceFileDirectory;
        }
        
        public void Run()
        {
            List<IParseTree> parseTrees = new List<IParseTree>();
            IEnumerable<string> files = SourceFileGetter.GetFilesInDirectory(sourceFileDirectory);

            foreach (string file in files)
            {
                ICharStream stream = CharStreams.fromPath(file);
                ITokenSource lexer = new DazelLexer(stream);
                ITokenStream tokens = new CommonTokenStream(lexer);
                DazelParser parser = new DazelParser(tokens) {BuildParseTree = true};
                
                parseTrees.Add(parser.start());
            }

            AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTrees);
        
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                AstPrinter astPrinter = new AstPrinter();
                astPrinter.Visit(gameObject);     
            }
            
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new TypeChecker(ast).Visit(gameObject);
            }
        }
    }
}