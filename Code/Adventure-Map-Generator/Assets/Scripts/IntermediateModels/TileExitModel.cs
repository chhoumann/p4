namespace Dazel.IntermediateModels
{
    public sealed class TileExitModel
    {
        public ScreenModel ToScreenModel { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
    }
}