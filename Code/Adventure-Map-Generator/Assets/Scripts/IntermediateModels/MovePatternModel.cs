using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class MovePatternModel
    {
        public List<MoveData> MoveData { get; set; }

        public MovePatternBehavior MovePatternBehavior { get; set; } 
    }
}