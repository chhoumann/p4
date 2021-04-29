using System.Collections.Generic;

namespace IntermediateModels
{
    public sealed class MovePattern
    {
        public List<MoveData> MoveData { get; set; }

        public MovePatternBehavior MovePatternBehavior { get; set; } 
    }
}