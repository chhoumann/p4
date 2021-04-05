using System;

namespace Interpreter.Ast.Nodes.GameObjectNodes
{
    public sealed class ScreenType : GameObjectType
    {
        public override void PrintMe()
        {
            Console.WriteLine("Screen");
        }
    }
}