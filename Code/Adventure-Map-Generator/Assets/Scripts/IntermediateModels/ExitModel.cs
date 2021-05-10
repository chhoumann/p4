namespace IntermediateModels
{
    public sealed class ExitModel
    {
        public ScreenModel ToScreenModel { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
    }
}