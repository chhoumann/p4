using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class Circle : IGenerator
    {
        private readonly float radius;
        private readonly float centre;
        
        public IEnumerable<TileModel> Tiles { get; }
        
        public Circle(float radius, float centre)
        {
            this.radius = radius;
            this.centre = centre;
        }
    }
}