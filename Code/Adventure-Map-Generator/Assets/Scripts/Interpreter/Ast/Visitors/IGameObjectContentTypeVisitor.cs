﻿using Dazel.Interpreter.Ast.Nodes.GameObjectNodes.GameObjectContentTypes;

namespace Dazel.Interpreter.Ast.Visitors
{
    public interface IGameObjectContentTypeVisitor
    {
        void Visit(MapTypeNode mapTypeNode);
        void Visit(OnScreenEnteredTypeNode onScreenEnteredTypeNode);
        void Visit(DataTypeNodeNode dataTypeNodeNode);
        void Visit(EntitiesTypeNodeNode entitiesTypeNodeNode);
        void Visit(ExitsTypeNodeNode exitsTypeNodeNode);
        void Visit(PatternTypeNode patternTypeNode);
    }
}