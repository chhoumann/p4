using System;
using System.Collections.Generic;
using Dazel.Interpreter.StandardLibrary.Functions.EntitiesFunctions;
using Dazel.Interpreter.StandardLibrary.Functions.ExitsFunctions;
using Dazel.Interpreter.StandardLibrary.Functions.MapFunctions;

namespace Dazel.Interpreter.StandardLibrary
{
    internal static class DazelStdLib
    {
        private static readonly Dictionary<string, Func<Function>> Functions = new Dictionary<string, Func<Function>>();

        static DazelStdLib()
        {
            AddMapFunctions();
            AddExitsFunctions();
            AddEntitiesFunctions();
        }

        public static bool TryGetFunction(string identifier, out Function function)
        {
            if (Functions.TryGetValue(identifier, out Func<Function> dazelFunction))
            {
                function = dazelFunction();
                return true;
            }

            function = null;

            return false;
        }
        
        private static void AddMapFunctions()
        {
            Functions.Add("Size", () => new SizeFunction());
            Functions.Add("Line", () => new LineFunction());
            Functions.Add("Floor", () => new FloorFunction());
            Functions.Add("Square", () => new SquareFunction());
            Functions.Add("Walls", () => new WallsFunction());
        }

        private static void AddExitsFunctions()
        {
            Functions.Add("Exit", () => new ExitFunction());
            Functions.Add("FloorExits", () => new FloorExitsFunction());
            Functions.Add("ScreenExit", () => new ScreenExitFunction());
        }

        private static void AddEntitiesFunctions()
        {
            Functions.Add("SpawnEntity", () => new SpawnEntityFunction());
        }
    }
}