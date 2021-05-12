namespace Dazel.IntermediateModels
{
    public sealed class ScreenExitModel
    {
        public ScreenModel ToScreenModel { get; set; }
        public Direction ExitDirection { get; set; }   
    }
}