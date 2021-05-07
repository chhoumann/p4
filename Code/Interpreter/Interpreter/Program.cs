namespace Interpreter
{
    public static class Program
    {
        private const string SourceFileDirectory = @".\Antlr\";
        
        private static void Main(string[] args)
        {
            new DazelInterpreter(SourceFileDirectory).Run();
            //Console.WriteLine(SymbolTable.Instance.Scopes[0].RetrieveSymbol("SomeVar"));

            // Front-end
            // Lexer -> Parser -> Build Parse Tree -> Transform to AST -> Static Semantic Analysis (Symbol Table)
            // Back-end
            // Interpretation hooked up to Dazel-Game API
        }
    }
}