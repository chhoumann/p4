namespace Dazel.IntermediateModels
{
    public sealed class TileModel
    {
        public int X { get; }
        public int Y { get; }

        public string GraphicName { get; }

        public TileModel(int x, int y, string graphicName)
        {
            X = x;
            Y = y;
            GraphicName = graphicName;
        }
    }
}