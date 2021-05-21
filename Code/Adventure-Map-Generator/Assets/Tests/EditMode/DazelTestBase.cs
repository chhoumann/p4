using Dazel.Compiler.ErrorHandler;
using Dazel.Compiler.SemanticAnalysis;
using NUnit.Framework;

namespace Tests.EditMode
{
    public abstract class DazelTestBase
    {
        [SetUp]
        public void CleanUp()
        {
            EnvironmentStore.CleanUp();
            DazelLogger.HasErrors = false;
            DazelLogger.ThrowExceptions = false;
        }
    }
}