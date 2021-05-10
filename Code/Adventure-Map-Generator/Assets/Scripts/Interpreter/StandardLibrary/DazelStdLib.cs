using System.Collections.Generic;
using Dazel.Interpreter.StandardLibrary.Functions.EntitiesFunctions;
using Dazel.Interpreter.StandardLibrary.Functions.ExitsFunctions;
using Dazel.Interpreter.StandardLibrary.Functions.MapFunctions;

namespace Dazel.Interpreter.StandardLibrary
{
    internal static class DazelStdLib
    {
        public static readonly Dictionary<string, Function> Functions = new Dictionary<string, Function>();

        static DazelStdLib()
        {
            AddMapFunctions();
            AddExitsFunctions();
            AddEntitiesFunctions();
        }

        private static void AddMapFunctions()
        {
            Functions.Add("Size", new SizeFunction());
            Functions.Add("Line", new LineFunction());
            Functions.Add("Floor", new FloorFunction());
            Functions.Add("Square", new SquareFunction());
            Functions.Add("Walls", new WallsFunction());
        }

        private static void AddExitsFunctions()
        {
            Functions.Add("Exit", new ExitFunction());
            Functions.Add("FloorExits", new FloorExitsFunction());
            Functions.Add("ScreenExit", new ScreenExitFunction());
        }

        private static void AddEntitiesFunctions()
        {
            Functions.Add("SpawnEntity", new SpawnEntityFunction());
        }
    }
}