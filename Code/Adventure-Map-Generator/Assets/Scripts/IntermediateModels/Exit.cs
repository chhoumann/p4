namespace P4.MapGenerator.IntermediateModels
{
    public sealed class Exit
    {
        public Screen ToScreen { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
    }
}