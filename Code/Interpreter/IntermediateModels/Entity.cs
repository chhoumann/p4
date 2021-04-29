namespace IntermediateModels
{
    public sealed class Entity
    {
        public MovePattern MovePattern { get; set; }

        public string Name { get; set; }

        public float Health { get; set; }
        public float Damage { get; set; }
        public float Range { get; set; }
    }
}