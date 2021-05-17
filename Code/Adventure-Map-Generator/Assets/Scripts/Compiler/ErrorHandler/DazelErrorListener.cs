using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;

namespace Dazel.Compiler.ErrorHandler
{
    public sealed class DazelErrorListener : BaseErrorListener
    {
        private DazelLogger dazelLogger;

        public DazelErrorListener(DazelLogger dazelLogger)
        {
            this.dazelLogger = dazelLogger;
        }

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            StringBuilder errorLog = new StringBuilder();
            base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            
            IEnumerable<string> reversedInvocationStack = ((Parser) recognizer).GetRuleInvocationStack().Reverse();
            string underlineError = UnderlineError(recognizer, offendingSymbol, line, charPositionInLine);

            errorLog.AppendLine($"File: {recognizer.InputStream.SourceName}");
            errorLog.AppendLine($"Line {line}:{charPositionInLine} at {offendingSymbol}: {msg}\n");
            errorLog.AppendLine($"Error: \n{underlineError}");

            string ruleStack = string.Join("\n", reversedInvocationStack);
            
            dazelLogger.EmitError(errorLog.ToString());
        }

        private string UnderlineError(IRecognizer recognizer, IToken offendingToken, int line, int charPositionInLine)
        {
            StringBuilder sb = new StringBuilder();
            CommonTokenStream tokens = (CommonTokenStream) recognizer.InputStream;
            
            string input = tokens.TokenSource.InputStream.ToString();
            string[] lines = input.Split('\n');
            string errorLine = lines[line - 1];
            sb.AppendLine(errorLine.TrimStart());

            sb.Append(new string(' ', charPositionInLine));

            int start = offendingToken.StartIndex;
            int stop = offendingToken.StopIndex;

            if (start >= 0 && stop >= 0)
            {
                int count = stop - start; 
                sb.Append(new string('^', count));
            }

            return sb.ToString();
        }
    }
}