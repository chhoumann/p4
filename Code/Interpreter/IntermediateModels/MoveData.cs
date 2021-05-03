namespace IntermediateModels
{
    public readonly struct MoveData
    {
        public readonly Direction Direction;

        public readonly int MoveAmount;

        public MoveData(Direction direction, int moveAmount)
        {
            Direction = direction;
            MoveAmount = moveAmount;
        }
    }
}