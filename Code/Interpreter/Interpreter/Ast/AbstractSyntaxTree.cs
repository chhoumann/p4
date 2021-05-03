using System;
using System.Collections.Generic;
using Interpreter.Ast.Nodes;
using Interpreter.Ast.Nodes.ExpressionNodes.Values;
using Interpreter.Ast.Nodes.GameObjectNodes;
using Interpreter.Ast.Nodes.StatementNodes;

namespace Interpreter.Ast
{
    internal sealed class AbstractSyntaxTree
    {
        public RootNode Root { get; }

        public static AbstractSyntaxTree Instance { get; private set; }
        
        public AbstractSyntaxTree(RootNode root)
        {
            if (Instance != null)
            {
                Instance = this;
            }

            Root = root;
        }

        public bool TryRetrieveNode(List<string> identifierList, out ValueNode node)
        {
            // TODO: Temporary handling for player member access.
            if (identifierList[0] == "Player" && identifierList[1] == "Health")
            {
                node = new IntValue() {Value = 100};
                return true;
            }

            GameObject start = Root.GameObjects[identifierList[0]];
            foreach (GameObjectContent gameObjectContent in start.Contents)
            {
                if (nameof(gameObjectContent.Type) == identifierList[1])
                {
                    foreach (StatementNode statementNode in gameObjectContent.Statements)
                    {
                        if (statementNode is AssignmentNode assignmentNode &&
                            assignmentNode.Identifier == identifierList[2])
                        {
                            node = assignmentNode.Expression as ValueNode;
                            return true;
                        }
                    }
                }
            }

            node = default;
            return false;
        }

        public bool TryRetrieveGameObject(string identifier, out GameObject gameObject)
        {
            return Root.GameObjects.TryGetValue(identifier, out gameObject);
        }
    }
}