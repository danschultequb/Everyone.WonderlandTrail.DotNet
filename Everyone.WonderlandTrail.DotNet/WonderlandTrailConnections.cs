using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Everyone
{
    [DebuggerDisplay($"{nameof(Count)}: {{{nameof(Count)}}}")]
    public class WonderlandTrailConnections
    {
        private IDictionary<WonderlandTrailLocation, ISet<WonderlandTrailConnection>> connectionMap;

        private WonderlandTrailConnections(bool addReverseConnectionDefault)
        {
            this.connectionMap = new Dictionary<WonderlandTrailLocation, ISet<WonderlandTrailConnection>>();
            this.AddReverseConnectionDefault = addReverseConnectionDefault;
        }

        public static WonderlandTrailConnections Create(bool addReverseConnectionDefault = false)
        {
            return new WonderlandTrailConnections(
                addReverseConnectionDefault: addReverseConnectionDefault);
        }

        public static WonderlandTrailConnections CreateDefault()
        {
            return WonderlandTrailConnections.Create(addReverseConnectionDefault: true)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.WhiteRiver,
                    endLocation: WonderlandTrailLocations.FryingPanCreek,
                    distanceMiles: 2.7,
                    ascentFeet: 100,
                    descentFeet: 600)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.FryingPanCreek,
                    endLocation: WonderlandTrailLocations.Summerland,
                    distanceMiles: 4.3,
                    ascentFeet: 2200,
                    descentFeet: 100)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.Summerland,
                    endLocation: WonderlandTrailLocations.IndianBar,
                    distanceMiles: 4.7,
                    ascentFeet: 1200,
                    descentFeet: 2100)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.IndianBar,
                    endLocation: WonderlandTrailLocations.NickelCreek,
                    distanceMiles: 6.8,
                    ascentFeet: 1400,
                    descentFeet: 3200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.NickelCreek,
                    endLocation: WonderlandTrailLocations.BoxCanyon,
                    distanceMiles: 0.9,
                    ascentFeet: 100,
                    descentFeet: 400)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.BoxCanyon,
                    endLocation: WonderlandTrailLocations.MapleCreek,
                    distanceMiles: 2.7,
                    ascentFeet: 500,
                    descentFeet: 700)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.MapleCreek,
                    endLocation: WonderlandTrailLocations.ReflectionLakes,
                    distanceMiles: 4.7,
                    ascentFeet: 2300,
                    descentFeet: 200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.ReflectionLakes,
                    endLocation: WonderlandTrailLocations.ParadiseRiver,
                    distanceMiles: 2.6,
                    ascentFeet: 200,
                    descentFeet: 1200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.ParadiseRiver,
                    endLocation: WonderlandTrailLocations.Longmire,
                    distanceMiles: 3.6,
                    ascentFeet: 100,
                    descentFeet: 1200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.Longmire,
                    endLocation: WonderlandTrailLocations.PyramidCreek,
                    distanceMiles: 3.3,
                    ascentFeet: 1400,
                    descentFeet: 400)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.PyramidCreek,
                    endLocation: WonderlandTrailLocations.DevilsDream,
                    distanceMiles: 2.5,
                    ascentFeet: 1400,
                    descentFeet: 100)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.DevilsDream,
                    endLocation: WonderlandTrailLocations.SouthPuyallupRiver,
                    distanceMiles: 6.5,
                    ascentFeet: 1900,
                    descentFeet: 2700)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.SouthPuyallupRiver,
                    endLocation: WonderlandTrailLocations.KlapatchePark,
                    distanceMiles: 4.1,
                    ascentFeet: 2100,
                    descentFeet: 800)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.KlapatchePark,
                    endLocation: WonderlandTrailLocations.NorthPuyallupRiver,
                    distanceMiles: 2.6,
                    ascentFeet: 100,
                    descentFeet: 1900)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.NorthPuyallupRiver,
                    endLocation: WonderlandTrailLocations.GoldenLakes,
                    distanceMiles: 5.1,
                    ascentFeet: 1900,
                    descentFeet: 600)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.GoldenLakes,
                    endLocation: WonderlandTrailLocations.SouthMowichRiver,
                    distanceMiles: 5.8,
                    ascentFeet: 200,
                    descentFeet: 2400)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.SouthMowichRiver,
                    endLocation: WonderlandTrailLocations.MowichLake,
                    distanceMiles: 4.4,
                    ascentFeet: 2400,
                    descentFeet: 300)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.MowichLake,
                    endLocation: WonderlandTrailLocations.IpsutCreek,
                    distanceMiles: 5.6,
                    ascentFeet: 400,
                    descentFeet: 3000)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.IpsutCreek,
                    endLocation: WonderlandTrailLocations.CarbonRiver,
                    distanceMiles: 4,
                    ascentFeet: 1300,
                    descentFeet: 400)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.MowichLake,
                    endLocation: WonderlandTrailLocations.EaglesRoost,
                    distanceMiles: 2,
                    ascentFeet: 500,
                    descentFeet: 600)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.EaglesRoost,
                    endLocation: WonderlandTrailLocations.CataractValley,
                    distanceMiles: 12.4,
                    ascentFeet: 3300,
                    descentFeet: 3700)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.CataractValley,
                    endLocation: WonderlandTrailLocations.CarbonRiver,
                    distanceMiles: 1.6,
                    ascentFeet: 100,
                    descentFeet: 1300)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.CarbonRiver,
                    endLocation: WonderlandTrailLocations.DickCreek,
                    distanceMiles: 1.3,
                    ascentFeet: 1000,
                    descentFeet: 100)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.DickCreek,
                    endLocation: WonderlandTrailLocations.MysticLake,
                    distanceMiles: 3.7,
                    ascentFeet: 2100,
                    descentFeet: 600)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.MysticLake,
                    endLocation: WonderlandTrailLocations.GraniteCreek,
                    distanceMiles: 4.1,
                    ascentFeet: 1500,
                    descentFeet: 1200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.GraniteCreek,
                    endLocation: WonderlandTrailLocations.SunriseCamp,
                    distanceMiles: 4.6,
                    ascentFeet: 1400,
                    descentFeet: 1000)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.SunriseCamp,
                    endLocation: WonderlandTrailLocations.WhiteRiver,
                    distanceMiles: 3.5,
                    ascentFeet: 100,
                    descentFeet: 2100)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.SunriseCamp,
                    endLocation: WonderlandTrailLocations.SunriseVisitorCenter,
                    distanceMiles: 1.4,
                    ascentFeet: 400,
                    descentFeet: 200)
                .AddConnection(
                    startLocation: WonderlandTrailLocations.SunriseVisitorCenter,
                    endLocation: WonderlandTrailLocations.WhiteRiver,
                    distanceMiles: 3.1,
                    ascentFeet: 0,
                    descentFeet: 2200);
        }

        public bool AddReverseConnectionDefault { get; set; }

        public int Count => this.GetConnections().Count();

        public IEnumerable<WonderlandTrailConnection> GetConnections(
            WonderlandTrailLocation? startLocation = null,
            WonderlandTrailLocation? endLocation = null,
            WonderlandTrailDirection? direction = null)
        {
            IEnumerable<WonderlandTrailConnection> result;
            
            if (startLocation == null)
            {
                result = this.connectionMap.Values.SelectMany(x => x);
            }
            else
            {
                result = this.connectionMap.TryGetValue(startLocation, out ISet<WonderlandTrailConnection>? connections) && connections != null
                    ? connections
                    : Enumerable.Empty<WonderlandTrailConnection>();
            }

            if (endLocation != null)
            {
                result = result.Where(connection => connection.EndLocation == endLocation);
            }

            if (direction != null)
            {
                result = result.Where(connection => connection.Direction == direction);
            }

            return result;
        }

        private void AddConnectionInner(WonderlandTrailConnection connection)
        {
            ISet<WonderlandTrailConnection>? locationConnections;
            if (!this.connectionMap.TryGetValue(connection.StartLocation, out locationConnections) || locationConnections == null)
            {
                locationConnections = new HashSet<WonderlandTrailConnection>();
                this.connectionMap.Add(connection.StartLocation, locationConnections);
            }
            locationConnections.Add(connection);
        }

        public WonderlandTrailConnections AddConnection(
            WonderlandTrailLocation startLocation,
            WonderlandTrailLocation endLocation,
            double distanceMiles,
            int ascentFeet,
            int descentFeet,
            WonderlandTrailDirection direction = WonderlandTrailDirection.Clockwise,
            IEnumerable<WonderlandTrailLocation>? intermediateLocations = null,
            bool? addReverseConnection = null)
        {
            return this.AddConnection(
                connection: WonderlandTrailConnection.Create(
                    startLocation: startLocation,
                    endLocation: endLocation,
                    distanceMiles: distanceMiles,
                    ascentFeet: ascentFeet,
                    descentFeet: descentFeet,
                    direction: direction,
                    intermediateLocations: intermediateLocations),
                addReverseConnection: addReverseConnection);
        }

        public WonderlandTrailConnections AddConnection(WonderlandTrailConnection connection, bool? addReverseConnection = null)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            this.AddConnectionInner(connection);
            if (addReverseConnection ?? this.AddReverseConnectionDefault)
            {
                this.AddConnectionInner(connection.ReverseDirection());
            }

            return this;
        }

        public bool ContainsConnection(WonderlandTrailConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            return this.connectionMap.TryGetValue(connection.StartLocation, out ISet<WonderlandTrailConnection>? connections) &&
                connections?.Contains(connection) == true;
        }

        public WonderlandTrailConnections ReverseDirection()
        {
            WonderlandTrailConnections result = WonderlandTrailConnections.Create(addReverseConnectionDefault: this.AddReverseConnectionDefault);

            foreach (WonderlandTrailConnection connection in this.GetConnections())
            {
                result.AddConnection(connection.ReverseDirection(), addReverseConnection: false);
            }

            return result;
        }

        public WonderlandTrailConnections ExpandConnections(
            WonderlandTrailLocation? startLocation = null,
            WonderlandTrailLocation? endLocation = null,
            WonderlandTrailDirection direction = WonderlandTrailDirection.Clockwise)
        {
            WonderlandTrailConnections result = WonderlandTrailConnections.Create(
                addReverseConnectionDefault: this.AddReverseConnectionDefault);

            Stack<WonderlandTrailConnection> toVisit = new Stack<WonderlandTrailConnection>(this.GetConnections(
                startLocation: startLocation,
                direction: direction));
            while (toVisit.Count > 0)
            {
                WonderlandTrailConnection currentConnection = toVisit.Pop();
                result.AddConnection(currentConnection, addReverseConnection: false);

                if (!currentConnection.IsLoop() && currentConnection.EndLocation != endLocation)
                {
                    IEnumerable<WonderlandTrailConnection> endLocationConnections = this.GetConnections(
                        startLocation: currentConnection.EndLocation,
                        direction: direction);
                    foreach (WonderlandTrailConnection endLocationConnection in endLocationConnections)
                    {
                        if (!result.ContainsConnection(endLocationConnection) &&
                            !toVisit.Contains(endLocationConnection))
                        {
                            toVisit.Push(endLocationConnection);
                        }

                        if (!currentConnection.IntermediateLocations.Contains(endLocationConnection.EndLocation))
                        {
                            WonderlandTrailConnection newConnection = currentConnection.Join(endLocationConnection);
                            toVisit.Push(newConnection);
                        }
                    }
                }
            }

            return result;
        }
    }
}
