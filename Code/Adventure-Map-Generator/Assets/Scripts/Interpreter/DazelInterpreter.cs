using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.IntermediateModels;
using Dazel.Interpreter.Ast;
using Dazel.Interpreter.Ast.Nodes.GameObjectNodes;
using Dazel.Interpreter.CodeGeneration;
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
        
        public IEnumerable<ScreenModel> Run()
        {
            IEnumerable<IParseTree> parseTrees = BuildParseTrees();

            AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTrees);
        
            PrintAst(ast);
            PerformSemanticAnalysis(ast);
            
            return GenerateIntermediateModels(ast);
        }

        private IEnumerable<IParseTree> BuildParseTrees()
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

            return parseTrees;
        }

        private static void PrintAst(AbstractSyntaxTree ast)
        {
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                AstPrinter astPrinter = new AstPrinter();
                astPrinter.Visit(gameObject);
            }
        }

        private static void PerformSemanticAnalysis(AbstractSyntaxTree ast)
        {
            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                new TypeChecker(ast).Visit(gameObject);
            }
        }

        private static IEnumerable<ScreenModel> GenerateIntermediateModels(AbstractSyntaxTree ast)
        {
            List<ScreenModel> screenModels = new List<ScreenModel>();

            foreach (GameObjectNode gameObject in ast.Root.GameObjects.Values)
            {
                switch (gameObject.TypeNode)
                {
                    case ScreenNode screenNode:
                        screenModels.Add(new ScreenGenerator(gameObject).Generate());
                        break;
                }
            }

            return screenModels;
        }
    }
}