using System;
using System.Collections.Generic;
using System.Linq;

namespace Everyone
{
    public class WonderlandTrailItinerary
    {
        private readonly List<WonderlandTrailConnection> connections;
        private readonly List<WonderlandTrailAvailabilityType> availabilityTypes;
        private WonderlandTrailItinerary(DateTime startDay)
        {
            this.StartDay = startDay;
            this.connections = new List<WonderlandTrailConnection>();
            this.availabilityTypes = new List<WonderlandTrailAvailabilityType>();
        }

        public static WonderlandTrailItinerary Create(DateTime startDay)
        {
            return new WonderlandTrailItinerary(startDay);
        }

        public WonderlandTrailItinerary Clone()
        {
            return WonderlandTrailItinerary.Create(this.StartDay)
                .AddConnectionRange(this.connections)
                .AddAvailabilityTypeRange(this.availabilityTypes);
        }

        public DateTime StartDay { get; set; }

        public IEnumerable<WonderlandTrailConnection> Connections => this.connections;

        public DateTime GetEndDay()
        {
            return this.StartDay.AddDays(this.GetDayCount() - 1);
        }

        public int GetDayCount()
        {
            return this.connections.Count;
        }

        public WonderlandTrailLocation? GetStartLocation()
        {
            return this.Connections.FirstOrDefault()?.StartLocation;
        }

        public WonderlandTrailLocation? GetEndLocation()
        {
            return this.Connections.LastOrDefault()?.EndLocation;
        }

        public bool ContainsAny(
            WonderlandTrailConnection connection,
            bool checkConnectionStartLocation = true,
            bool checkConnectionIntermediateLocations = true,
            bool checkConnectionEndLocation = true,
            bool checkItineraryStartLocation = true,
            bool checkItineraryIntermediateLocations = true,
            bool checkItineraryEndLocation = true)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            bool result = false;

            if (!result && checkConnectionStartLocation)
            {
                result = this.Contains(connection.StartLocation,
                    checkItineraryStartLocation: checkItineraryStartLocation,
                    checkItineraryIntermediateLocations: checkItineraryIntermediateLocations,
                    checkItineraryEndLocation: checkItineraryEndLocation);
            }

            if (!result && checkConnectionIntermediateLocations)
            {
                result = connection.IntermediateLocations.Any(cil => this.Contains(cil,
                    checkItineraryStartLocation: checkItineraryStartLocation,
                    checkItineraryIntermediateLocations: checkItineraryIntermediateLocations,
                    checkItineraryEndLocation: checkItineraryEndLocation));
            }

            if (!result && checkConnectionEndLocation)
            {
                result = this.Contains(connection.EndLocation,
                    checkItineraryStartLocation: checkItineraryStartLocation,
                    checkItineraryIntermediateLocations: checkItineraryIntermediateLocations,
                    checkItineraryEndLocation: checkItineraryEndLocation);
            }

            return result;
        }

        public bool Contains(
            WonderlandTrailLocation location,
            bool checkItineraryStartLocation = true,
            bool checkItineraryIntermediateLocations = true,
            bool checkItineraryEndLocation = true)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            bool result = false;

            for (int i = 0; i < this.connections.Count; i++)
            {
                if (i != 0 || checkItineraryStartLocation)
                {
                    result = this.connections[i].StartLocation == location;
                    if (result)
                    {
                        break;
                    }
                }

                if (checkItineraryIntermediateLocations)
                {
                    result = this.connections[i].IntermediateLocations.Contains(location);
                    if (result)
                    {
                        break;
                    }
                }

                if (i == this.connections.Count - 1 && checkItineraryEndLocation)
                {
                    result = this.connections[i].EndLocation == location;
                    if (result)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public IEnumerable<WonderlandTrailLocation> GetIntermediateLocations()
        {
            List<WonderlandTrailLocation> result = new List<WonderlandTrailLocation>();
            foreach (WonderlandTrailConnection connection in this.connections)
            {
                if (result.Any())
                {
                    result.Add(connection.StartLocation);
                }
                result.AddRange(connection.IntermediateLocations);
            }
            return result;
        }

        public IEnumerable<WonderlandTrailLocation> Path
        {
            get
            {
                List<WonderlandTrailLocation> result = new List<WonderlandTrailLocation>();
                foreach (WonderlandTrailConnection connection in this.connections)
                {
                    if (!result.Any())
                    {
                        result.Add(connection.StartLocation);
                    }
                    result.Add(connection.EndLocation);
                }
                return result;
            }
        }

        public IEnumerable<string> PathStrings => this.GetPathStrings(includeAvailabilityTypes: true);

        public IEnumerable<string> GetPathStrings(bool includeAvailabilityTypes)
        {
            List<string> result = new List<string>();
            int availabilityTypeIndex = 0;
            foreach (WonderlandTrailConnection connection in this.connections)
            {
                if (!result.Any())
                {
                    result.Add(connection.StartLocation.Name);
                }

                string pathString = connection.EndLocation.Name;
                if (includeAvailabilityTypes && availabilityTypeIndex < this.availabilityTypes.Count)
                {
                    pathString += " ";

                    WonderlandTrailAvailabilityType availabilityType = this.availabilityTypes[availabilityTypeIndex];
                    availabilityTypeIndex++;

                    if (availabilityType.IndividualSite != null)
                    {
                        if (availabilityType.GroupSite != null)
                        {
                            pathString += $"(Individual {availabilityType.IndividualSite}/Group {availabilityType.GroupSite})";
                        }
                        else
                        {
                            pathString += $"(Individual {availabilityType.IndividualSite})";
                        }
                    }
                    else
                    {
                        pathString += $"(Group {availabilityType.GroupSite})";
                    }
                }

                result.Add(pathString);
            }
            return result;
        }

        public WonderlandTrailItinerary AddConnection(WonderlandTrailConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            this.connections.Add(connection);

            return this;
        }

        public WonderlandTrailItinerary AddConnectionRange(IEnumerable<WonderlandTrailConnection> connections)
        {
            if (connections == null)
            {
                throw new ArgumentNullException(nameof(connections));
            }

            foreach (WonderlandTrailConnection connection in connections)
            {
                this.AddConnection(connection);
            }

            return this;
        }

        public WonderlandTrailItinerary AddAvailabilityType(WonderlandTrailAvailabilityType availabilityType)
        {
            if (availabilityType == null)
            {
                throw new ArgumentNullException(nameof(availabilityType));
            }

            this.availabilityTypes.Add(availabilityType);

            return this;
        }

        public WonderlandTrailItinerary AddAvailabilityTypeRange(IEnumerable<WonderlandTrailAvailabilityType> availabilityTypes)
        {
            if (availabilityTypes == null)
            {
                throw new ArgumentNullException(nameof(availabilityTypes));
            }

            foreach (WonderlandTrailAvailabilityType availabilityType in availabilityTypes)
            {
                this.AddAvailabilityType(availabilityType);
            }

            return this;
        }

        public override string ToString()
        {
            return this.ToString(includeAvailabilityTypes: true);
        }

        public string ToString(bool includeAvailabilityTypes)
        {
            return $"{nameof(this.StartDay)}:{this.StartDay.ToShortDateString()},Path:{Enumerables.ToString(this.GetPathStrings(includeAvailabilityTypes))}";
        }
    }
}
