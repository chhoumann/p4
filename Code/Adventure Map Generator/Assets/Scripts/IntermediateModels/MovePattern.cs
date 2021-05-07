using System.Collections.Generic;

namespace P4.MapGenerator.IntermediateModels
{
    public sealed class MovePattern
    {
        public List<MoveData> MoveData { get; set; }

        public MovePatternBehavior MovePatternBehavior { get; set; } 
    }
}