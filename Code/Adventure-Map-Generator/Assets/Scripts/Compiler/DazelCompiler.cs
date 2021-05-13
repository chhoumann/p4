﻿using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Dazel.Compiler.Ast;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.CodeGeneration;
using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using Dazel.IntermediateModels;

namespace Dazel.Compiler
{
    public sealed class DazelCompiler
    {
        private readonly string sourceFileDirectory;
        private readonly IErrorLogger errorLogger;

        public DazelCompiler(string sourceFileDirectory)
        {
            this.sourceFileDirectory = sourceFileDirectory;
            errorLogger = new DazelErrorLogger();
        }
        
        public IEnumerable<ScreenModel> Run()
        {
            try
            {
                IEnumerable<IParseTree> parseTrees = BuildParseTrees();

                AbstractSyntaxTree ast = new AstBuilder().BuildAst(parseTrees);

                if (errorLogger.HasErrors)
                {
                    throw new Exception("Invalid Dazel code.");
                }
                
                PrintAst(ast);
                PerformSemanticAnalysis(ast);

                return GenerateIntermediateModels(ast);
            }
            catch (Exception e)
            {
                errorLogger.AddToErrorList(e.Message);
            }
            finally
            {
                errorLogger.Log();
            }

            return null;
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
                parser.AddErrorListener(new DazelErrorListener(errorLogger));

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