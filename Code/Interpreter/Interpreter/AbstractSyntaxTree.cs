using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;

namespace Interpreter
{
    public abstract class Node
    {
        public string Text { get; }
        
        public List<Node> Children { get; }

        public Node(string text)
        {
            Text = text;
        }
    }
    
    public sealed class AbstractSyntaxTree : IVisitor
    {
        private List<IParseTree> terminalNodes = new List<IParseTree>();
        private List<Node> nodes = new List<Node>();
        
        public AbstractSyntaxTree(IParseTree parseTree)
        {
            GetTerminal(parseTree);

            Console.WriteLine(terminalNodes.Count);
            
            for (int i = 0; i < terminalNodes.Count; i++)
            {
                IParseTree terminalNode = terminalNodes[i];
                string text = terminalNode.GetText().Replace(" ", string.Empty);

                switch (text)
                {
                    case "Screen":
                        nodes.Add(new GameObject(terminalNode.GetText() + terminalNodes[i + 1].GetText()));
                        break;
                    case "Map":
                        nodes.Add(new GameObjectContents(terminalNode.GetText()));
                        break;
                    case "Entities":
                        nodes.Add(new GameObjectContents(terminalNode.GetText()));
                        break;
                    case "Exits":
                        nodes.Add(new GameObjectContents(terminalNode.GetText()));
                        break;
                }
            }
            
            foreach (Node node in nodes)
            {
                Console.WriteLine(node.Text);
            }
        }

        private void GetTerminal(IParseTree parseTree)
        {
            for (int i = 0; i < parseTree.ChildCount; i++)
            {
                IParseTree node = parseTree.GetChild(i);

                if (node.ChildCount == 0)
                {
                    terminalNodes.Add(node);
                }
                else
                {
                    GetTerminal(node);
                }
            }
        }
        
        public void Visit(GameObject gameObject)
        {
            
        }

        public void Visit(GameObjectType gameObjectType)
        {
        }

        public void Visit(GameObjectContents gameObjectContents)
        {
        }
    }
}