using System.Collections.Generic;
using P4.MapGenerator.Interpreter.Ast.Nodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.ExpressionNodes.Values;
using P4.MapGenerator.Interpreter.Ast.Nodes.GameObjectNodes;
using P4.MapGenerator.Interpreter.Ast.Nodes.StatementNodes;

namespace P4.MapGenerator.Interpreter.Ast
{
    public sealed class AbstractSyntaxTree
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
                node = new IntValueNode() {Value = 100};
                return true;
            }

            GameObjectNode start = Root.GameObjects[identifierList[0]];
            foreach (GameObjectContentNode gameObjectContent in start.Contents)
            {
                if (nameof(gameObjectContent.TypeNode) == identifierList[1])
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

        public bool TryRetrieveGameObject(string identifier, out GameObjectNode gameObjectNode)
        {
            return Root.GameObjects.TryGetValue(identifier, out gameObjectNode);
        }
    }
}