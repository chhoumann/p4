namespace IntermediateModels
{
    public sealed class EntityModel
    {
        public MovePatternModel MovePatternModel { get; set; }

        public string Name { get; set; }

        public float Health { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }
    }
}