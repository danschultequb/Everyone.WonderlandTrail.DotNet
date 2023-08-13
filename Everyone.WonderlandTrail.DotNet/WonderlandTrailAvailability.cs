using System;
using System.Collections.Generic;

namespace Everyone
{
    public class WonderlandTrailAvailability
    {
        private readonly IDictionary<WonderlandTrailLocation, IDictionary<DateTime,WonderlandTrailAvailabilityType>> availabilityMap;
        private WonderlandTrailAvailability()
        {
            this.availabilityMap = new Dictionary<WonderlandTrailLocation, IDictionary<DateTime,WonderlandTrailAvailabilityType>>();
        }

        public static WonderlandTrailAvailability Create()
        {
            return new WonderlandTrailAvailability();
        }

        private static DateTime RemoveTime(DateTime dateTime)
        {
            return new DateTime(year: dateTime.Year, month: dateTime.Month, day: dateTime.Day);
        }

        public void AddAvailability(WonderlandTrailLocation location, DateTime dateTime, WonderlandTrailReservationType? individualSite, WonderlandTrailReservationType? groupSite)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            IDictionary<DateTime, WonderlandTrailAvailabilityType>? locationAvailability;
            if (!this.availabilityMap.TryGetValue(location, out locationAvailability) || locationAvailability == null)
            {
                locationAvailability = new Dictionary<DateTime, WonderlandTrailAvailabilityType>();
                this.availabilityMap.Add(location, locationAvailability);
            }

            WonderlandTrailAvailabilityType? locationDayAvailibility;
            if (!locationAvailability.TryGetValue(RemoveTime(dateTime), out locationDayAvailibility) || locationDayAvailibility == null)
            {
                locationDayAvailibility = new WonderlandTrailAvailabilityType();
                locationAvailability.Add(RemoveTime(dateTime), locationDayAvailibility);
            }

            if (individualSite != null)
            {
                locationDayAvailibility.IndividualSite = individualSite;
            }
            if (groupSite != null)
            {
                locationDayAvailibility.GroupSite = groupSite;
            }
        }

        public bool IsAvailable(WonderlandTrailLocation location, DateTime dateTime, out WonderlandTrailReservationType? individualSite, out WonderlandTrailReservationType? groupSite)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            WonderlandTrailAvailabilityType? availabilityType = null;
            bool result = this.availabilityMap.TryGetValue(location, out IDictionary<DateTime, WonderlandTrailAvailabilityType>? locationAvailability) &&
                locationAvailability != null &&
                locationAvailability.TryGetValue(RemoveTime(dateTime), out availabilityType);
            individualSite = availabilityType?.IndividualSite;
            groupSite = availabilityType?.GroupSite;

            return result;
        }
    }
}
