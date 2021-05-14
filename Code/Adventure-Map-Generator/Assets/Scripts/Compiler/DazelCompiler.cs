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
        private readonly IErrorLogger errorLogger;
        private readonly IEnumerable<string> files;
        private readonly bool fromFile;

        private DazelCompiler()
        {
            errorLogger = new DazelErrorLogger();
        }

        public DazelCompiler(string sourceFileDirectory) : this()
        {
            files = SourceFileGetter.GetFilesInDirectory(sourceFileDirectory);
            fromFile = true;
        }

        public DazelCompiler(params string[] files) : this()
        {
            this.files = files;
            fromFile = false;
        }
        
        public bool TryRun(out IEnumerable<ScreenModel> screenModels)
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

                screenModels = GenerateIntermediateModels(ast);
                return true;
            }
            catch (Exception e)
            {
                errorLogger.AddToErrorList(e.Message);
                errorLogger.AddToErrorList(e.StackTrace);
            }
            finally
            {
                errorLogger.Log();
            }

            screenModels = default;
            return false;
        }

        private IEnumerable<IParseTree> BuildParseTrees()
        {
            List<IParseTree> parseTrees = new List<IParseTree>();

            foreach (string file in files)
            {
                ICharStream stream = fromFile ? CharStreams.fromPath(file) : CharStreams.fromString(file);
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
                new LinkChecker(ast).Visit(gameObject);
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