using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everyone
{
    public class WonderlandTrailClient
    {
        private const string permitItineraryId = "4675317";

        private readonly RecreationDotGovClient client;

        private WonderlandTrailClient(RecreationDotGovClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public static WonderlandTrailClient Create()
        {
            return WonderlandTrailClient.Create(RecreationDotGovClient.Create());
        }

        public static WonderlandTrailClient Create(RecreationDotGovClient client)
        {
            return new WonderlandTrailClient(client);
        }

        public async Task<WonderlandTrailAvailability> GetAvailabilityAsync(
            int month,
            int year,
            bool allowWalkupPermits,
            bool allowIndividualSites,
            bool allowGroupSites)
        {
            WonderlandTrailAvailability result = WonderlandTrailAvailability.Create();

            foreach (WonderlandTrailLocation location in WonderlandTrailLocations.GetLocations())
            {
                if (allowIndividualSites && !string.IsNullOrEmpty(location.DivisionId))
                {
                    RecreationDotGovDivisionAvailability? divisionAvailability = await this.client.GetDivisionAvailabilityAsync(
                        permitItineraryId: WonderlandTrailClient.permitItineraryId,
                        divisionId: location.DivisionId,
                        month: month,
                        year: year);
                    IEnumerable<RecreationDotGovDivisionDayAvailability>? divisionDayAvailabilities = divisionAvailability?.DayAvailabilities;
                    if (divisionDayAvailabilities != null)
                    {
                        foreach (RecreationDotGovDivisionDayAvailability dayAvailability in divisionDayAvailabilities)
                        {
                            int? day = dayAvailability.Day;
                            bool hasWalkupPermits = (allowWalkupPermits && dayAvailability.Walkup == true);
                            bool hasReservationPermits = (dayAvailability.ReservationsRemaining != null && dayAvailability.ReservationsRemaining > 0);
                            bool available = (hasWalkupPermits || hasReservationPermits);
                            if (day.HasValue && available)
                            {
                                result.AddAvailability(
                                    location: location,
                                    dateTime: new DateTime(year: year, month: month, day: day.Value),
                                    individualSite: hasWalkupPermits
                                        ? WonderlandTrailReservationType.Walkup
                                        : WonderlandTrailReservationType.Reserved,
                                    groupSite: null);
                            }
                        }
                    }
                }

                if (allowGroupSites && !string.IsNullOrEmpty(location.GroupSiteDivisionId))
                {
                    RecreationDotGovDivisionAvailability? groupSiteDivisionAvailability = await this.client.GetDivisionAvailabilityAsync(
                        permitItineraryId: WonderlandTrailClient.permitItineraryId,
                        divisionId: location.GroupSiteDivisionId,
                        month: month,
                        year: year);
                    IEnumerable<RecreationDotGovDivisionDayAvailability>? groupSiteDivisionDayAvailabilities = groupSiteDivisionAvailability?.DayAvailabilities;
                    if (groupSiteDivisionDayAvailabilities != null)
                    {
                        foreach (RecreationDotGovDivisionDayAvailability groupSiteDayAvailability in groupSiteDivisionDayAvailabilities)
                        {
                            int? day = groupSiteDayAvailability.Day;
                            bool hasWalkupPermits = (allowWalkupPermits && groupSiteDayAvailability.Walkup == true);
                            bool hasReservationPermits = (groupSiteDayAvailability.ReservationsRemaining != null && groupSiteDayAvailability.ReservationsRemaining > 0);
                            bool available = (hasWalkupPermits || hasReservationPermits);
                            if (day.HasValue && available)
                            {
                                result.AddAvailability(
                                    location: location,
                                    dateTime: new DateTime(year: year, month: month, day: day.Value),
                                    individualSite: null,
                                    groupSite: hasWalkupPermits
                                        ? WonderlandTrailReservationType.Walkup
                                        : WonderlandTrailReservationType.Reserved);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static IEnumerable<WonderlandTrailItinerary> FindItineraries(
            WonderlandTrailAvailability availability,
            DateTime startDay,
            WonderlandTrailLocation startLocation,
            WonderlandTrailLocation endLocation,
            WonderlandTrailDirection direction,
            double? maximumDayDistanceMiles = null,
            int? maximumItineraryDays = null,
            IEnumerable<WonderlandTrailLocation>? campsitesToAvoid = null)
        {
            List<WonderlandTrailItinerary> result = new List<WonderlandTrailItinerary>();

            WonderlandTrailConnections connections = WonderlandTrailConnections.CreateDefault()
                .ExpandConnections(
                    startLocation: startLocation,
                    endLocation: endLocation,
                    direction: direction);

            IEnumerable<WonderlandTrailConnection> startLocationConnections = connections.GetConnections(
                startLocation: startLocation,
                direction: direction);
            if (maximumDayDistanceMiles != null)
            {
                startLocationConnections = startLocationConnections.Where(c => c.DistanceMiles <= maximumDayDistanceMiles);
            }
            Stack<WonderlandTrailItinerary> possibleItineraries = new Stack<WonderlandTrailItinerary>(
                startLocationConnections.Select(c => WonderlandTrailItinerary.Create(startDay).AddConnection(c)));
            while (possibleItineraries.Count > 0)
            {
                WonderlandTrailItinerary currentItinerary = possibleItineraries.Pop();
                WonderlandTrailLocation currentItineraryEndLocation = currentItinerary.GetEndLocation()!;

                if ((maximumItineraryDays == null || currentItinerary.GetDayCount() <= maximumItineraryDays) &&
                    (startLocation == currentItineraryEndLocation ||
                     endLocation == currentItineraryEndLocation ||
                     campsitesToAvoid?.Contains(currentItineraryEndLocation) != true))
                {
                    if (currentItineraryEndLocation == endLocation)
                    {
                        result.Add(currentItinerary);
                    }
                    else if (availability.IsAvailable(
                        location: currentItineraryEndLocation,
                        dateTime: currentItinerary.GetEndDay(),
                        out WonderlandTrailReservationType? individualSite,
                        out WonderlandTrailReservationType? groupSite))
                    {
                        currentItinerary.AddAvailabilityType(new WonderlandTrailAvailabilityType()
                        {
                            IndividualSite = individualSite,
                            GroupSite = groupSite,
                        });

                        IEnumerable<WonderlandTrailConnection> nextDayConnections = connections.GetConnections(
                            startLocation: currentItineraryEndLocation,
                            direction: direction);
                        foreach (WonderlandTrailConnection nextDayConnection in nextDayConnections)
                        {
                            if (!currentItinerary.ContainsAny(nextDayConnection,
                                checkConnectionStartLocation: false,
                                checkItineraryStartLocation: false,
                                checkItineraryEndLocation: false))
                            {
                                if (maximumDayDistanceMiles == null || nextDayConnection.DistanceMiles <= maximumDayDistanceMiles)
                                {
                                    possibleItineraries.Push(currentItinerary.Clone().AddConnection(nextDayConnection));
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<WonderlandTrailItinerary>> FindItinerariesAsync(
            DateTime startDay,
            WonderlandTrailLocation? startLocation = null,
            WonderlandTrailLocation? endLocation = null,
            WonderlandTrailDirection? direction = null,
            double? maximumDayDistanceMiles = null,
            int? maximumItineraryDays = null,
            bool allowWalkupPermits = true,
            bool allowIndividualSites = true,
            bool allowGroupSites = false,
            IEnumerable<WonderlandTrailLocation>? campsitesToAvoid = null)
        {
            List<WonderlandTrailItinerary> result = new List<WonderlandTrailItinerary>();

            List<WonderlandTrailLocation> startLocationsToCheck = new List<WonderlandTrailLocation>();
            if (startLocation != null)
            {
                startLocationsToCheck.Add(startLocation);
            }
            else
            {
                startLocationsToCheck.AddRange(WonderlandTrailLocations.GetTrailheads());
            }

            List<WonderlandTrailDirection> directionsToCheck = new List<WonderlandTrailDirection>();
            if (direction.HasValue)
            {
                directionsToCheck.Add(direction.Value);
            }
            else
            {
                directionsToCheck.Add(WonderlandTrailDirection.Clockwise);
                directionsToCheck.Add(WonderlandTrailDirection.CounterClockwise);
            }

            WonderlandTrailAvailability availability = await this.GetAvailabilityAsync(
                month: startDay.Month,
                year: startDay.Year,
                allowWalkupPermits: allowWalkupPermits,
                allowIndividualSites: allowIndividualSites,
                allowGroupSites: allowGroupSites);
            foreach (WonderlandTrailLocation startLocationToCheck in startLocationsToCheck)
            {
                foreach (WonderlandTrailDirection directionToCheck in directionsToCheck)
                {
                    result.AddRange(WonderlandTrailClient.FindItineraries(
                        availability: availability,
                        startDay: startDay,
                        startLocation: startLocationToCheck,
                        endLocation: endLocation ?? startLocationToCheck,
                        direction: directionToCheck,
                        maximumDayDistanceMiles: maximumDayDistanceMiles,
                        maximumItineraryDays: maximumItineraryDays,
                        campsitesToAvoid: campsitesToAvoid));
                }
            }

            return result;
        }
    }
}