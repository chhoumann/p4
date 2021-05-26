using Dazel.Compiler;
using Dazel.Compiler.ErrorHandler;
using NUnit.Framework;

namespace Tests.EditMode
{
    [TestFixture]
    public class CompilerRunTest : DazelTestBase
    {
	    private const string TestCode1_1 = @"Screen SampleScreen1
		{
			Map 
			{
				Size(80, 24);
				// comment
		        SomeVar1 = 2;
			    {
			        SomeVar2 = 3 + 3;
			        {
						SomeVar3 = 2 + 2;
			        }
			    }
			    
			    x = 4;
				Print(x);
				
				z = SampleScreen2.Exits.Exit1;
	    	    			    
				Walls(""Stone.png""); 
				Floor(""Grass"");
				
		        {
				    Line([2, 2], [2, 4], ""Cliff.png"");
		        }

				Square(position, size, ""Cliff.png"");
				Square([8, 8], 4, ""Cliff.png"");
			}

			Entities 
			{
				SpawnEntity(""Skeleton1"", [4, 5]);
				SpawnEntity(""Skeleton1"", [4, 6]);
			}
			
			Exits 
			{
				Exit1 = Exit([4, 0], SampleScreen2.Exits.Exit1);
				Print(""HasExit"");
				Print(Exit1);

				ScreenExit(Down, SampleScreen2);
				ScreenExit(Left, SampleScreen2);
				SomeVar = 3 + 3 + (3 + 3 + 3 + 3 * 3 * 2 * 1 * 5 * 1 - 3 + 8419);
				SomeVar = SomeVar + 3;
			}
		}";

	    private const string TestCode1_2 = @"Screen SampleScreen2
		{
			Map 
			{
				Size(20, 20);
		        
				Walls(""Grass.png""); 
				Floor(""Stone.png"");
			}
			
			Exits 
			{
				Exit1 = Exit([1,1], SampleScreen1.Exits.Exit1);
			}
		}";

        [Test]
        public void DazelCompiler_TryRun_SucceedOnValidSourceCode()
        {
            DazelCompiler dazelCompiler = new DazelCompiler(TestCode1_1, TestCode1_2);
            bool TestDelegate() => dazelCompiler.TryRun(out _);
            Assert.True(TestDelegate());
            Assert.DoesNotThrow(() => TestDelegate());
        }
        
        private const string TestCode2_1 =
            "Screen SampleScreen1" +
            "{" +
            "   Exits" +
            "   {" +
            "       s1exit1 = Exit([0, 0], SampleScreen2.Exits.s2exit1);" + 
            "   }" +
            "}";
        
        private const string TestCode2_2 = 
            "Screen SampleScreen2" +
            "{" +
            "   Exits" +
            "   {" +
            "   }" +
            "}";

        [Test]
        public void DazelCompiler_TryRun_ThrowOnInvalidSourceCode()
        {
            UnityEngine.TestTools.LogAssert.ignoreFailingMessages = true;
            DazelCompiler dazelCompiler = new DazelCompiler(TestCode2_1, TestCode2_2);
            DazelLogger.ThrowExceptions = false;
            
            Assert.False(dazelCompiler.TryRun(out _));
        }
    }
}