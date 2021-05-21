using System.Numerics;

namespace Dazel.IntermediateModels
{
    public sealed class EntityModel
    {
        public MovePatternModel MovePatternModel { get; set; }

        public string Identifier { get; set; }
        public Vector2 SpawnPosition { get; set; }

        public float Health { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }

        public EntityModel(string identifier, Vector2 spawnPosition)
        {
            Identifier = identifier;
            SpawnPosition = spawnPosition;
        }
    }
}