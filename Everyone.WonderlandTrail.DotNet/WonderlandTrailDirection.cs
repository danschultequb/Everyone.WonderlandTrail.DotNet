namespace Everyone
{
    public enum WonderlandTrailDirection
    {
        Clockwise,

        CounterClockwise,
    }

    public static class WonderlandTrailDirections
    {
        public static WonderlandTrailDirection Reverse(this WonderlandTrailDirection direction)
        {
            return direction == WonderlandTrailDirection.Clockwise
                ? WonderlandTrailDirection.CounterClockwise
                : WonderlandTrailDirection.Clockwise;
        }
    }
}
