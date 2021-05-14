using System.Collections.Generic;
using Dazel.Compiler.Ast.Nodes;
using Dazel.Compiler.Ast.Nodes.ExpressionNodes.Values;
using Dazel.Compiler.Ast.Nodes.GameObjectNodes;
using Dazel.Compiler.Ast.Nodes.StatementNodes;
using UnityEngine;

namespace Dazel.Compiler.Ast
{
    public sealed class AbstractSyntaxTree
    {
        public RootNode Root { get; }

        public AbstractSyntaxTree(RootNode root)
        {
            Root = root;
        }

        public bool TryRetrieveNode<TNodeType>(List<string> identifierList, out string identifier, out TNodeType node)
            where TNodeType : class
        {
            // TODO: Temporary handling for player member access.
            /*if (identifierList[0] == "Player" && identifierList[1] == "Health")
            {
                node = new IntValueNode() {Value = 100};
                return true;
            }*/

            GameObjectNode start = Root.GameObjects[identifierList[0]];
            foreach (GameObjectContentNode gameObjectContent in start.Contents)
            {
                if (gameObjectContent.TypeNode.ContentType.ToString() == identifierList[1])
                {
                    foreach (StatementNode statementNode in gameObjectContent.Statements)
                    {
                        if (statementNode is AssignmentNode assignmentNode &&
                            assignmentNode.Identifier == identifierList[2])
                        {
                            identifier = assignmentNode.Identifier;
                            node = assignmentNode.Expression as TNodeType;
                            return true;
                        }
                    }
                }
            }

            identifier = default;
            node = default;
            return false;
        }

        public bool TryRetrieveGameObject(string identifier, out GameObjectNode gameObjectNode)
        {
            return Root.GameObjects.TryGetValue(identifier, out gameObjectNode);
        }
    }
}