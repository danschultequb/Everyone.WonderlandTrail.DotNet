using System;

namespace Everyone
{
    public class WonderlandTrailDayConnection
    {
        private WonderlandTrailDayConnection(DateTime day, WonderlandTrailConnection connection)
        {
            this.Day = day;
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public static WonderlandTrailDayConnection Create(DateTime day, WonderlandTrailConnection connection)
        {
            return new WonderlandTrailDayConnection(day, connection);
        }

        public DateTime Day { get; }

        public WonderlandTrailConnection Connection { get; }
    }
}
