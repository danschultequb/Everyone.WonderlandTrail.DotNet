using System;
using System.Collections.Generic;
using System.Linq;

namespace Everyone
{
    public class WonderlandTrailConnection
    {
        private WonderlandTrailConnection(
            WonderlandTrailLocation startLocation,
            WonderlandTrailLocation endLocation,
            double distanceMiles,
            double ascentFeet,
            double descentFeet,
            WonderlandTrailDirection direction,
            IEnumerable<WonderlandTrailLocation>? intermediateLocations)
        {
            this.StartLocation = startLocation ?? throw new ArgumentNullException(nameof(startLocation));
            this.EndLocation = endLocation ?? throw new ArgumentNullException(nameof(endLocation));
            this.Direction = direction;
            this.DistanceMiles = distanceMiles;
            this.AscentFeet = ascentFeet;
            this.DescentFeet = descentFeet;
            this.IntermediateLocations = intermediateLocations ?? new WonderlandTrailLocation[0];
        }

        public static WonderlandTrailConnection Create(
            WonderlandTrailLocation startLocation,
            WonderlandTrailLocation endLocation,
            double distanceMiles, double ascentFeet,
            double descentFeet,
            WonderlandTrailDirection direction = WonderlandTrailDirection.Clockwise,
            IEnumerable<WonderlandTrailLocation>? intermediateLocations = null)
        {
            return new WonderlandTrailConnection(
                startLocation: startLocation,
                endLocation: endLocation,
                distanceMiles:distanceMiles,
                ascentFeet: ascentFeet,
                descentFeet: descentFeet,
                direction: direction,
                intermediateLocations: intermediateLocations);
        }

        public WonderlandTrailLocation StartLocation { get; }

        public WonderlandTrailLocation EndLocation { get; }

        public WonderlandTrailDirection Direction { get; }

        public double DistanceMiles { get; }

        public double AscentFeet { get; }

        public double DescentFeet { get; }

        public IEnumerable<WonderlandTrailLocation> IntermediateLocations { get; set; }

        public IEnumerable<WonderlandTrailLocation> GetLocations()
        {
            ISet<WonderlandTrailLocation> result = new HashSet<WonderlandTrailLocation>();
            result.Add(this.StartLocation);
            foreach (WonderlandTrailLocation location in this.IntermediateLocations)
            {
                result.Add(location);
            }
            result.Add(this.EndLocation);
            return result;
        }

        public WonderlandTrailConnection ReverseDirection()
        {
            return WonderlandTrailConnection.Create(
                startLocation: this.EndLocation,
                endLocation: this.StartLocation,
                distanceMiles: this.DistanceMiles,
                ascentFeet: this.DescentFeet,
                descentFeet: this.AscentFeet,
                direction: this.Direction.Reverse());
        }

        public WonderlandTrailConnection Join(WonderlandTrailConnection rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }
            if (this.EndLocation != rhs.StartLocation)
            {
                throw new ArgumentException($"The provided {nameof(WonderlandTrailConnection)}'s {nameof(rhs.StartLocation)} ({rhs.StartLocation.Name}) must be the same as this {nameof(WonderlandTrailConnection)}'s {nameof(this.EndLocation)} ({this.EndLocation.Name}).", nameof(rhs));
            }

            return WonderlandTrailConnection.Create(
                startLocation: this.StartLocation,
                endLocation: rhs.EndLocation,
                direction: this.Direction,
                distanceMiles: this.DistanceMiles + rhs.DistanceMiles,
                ascentFeet: this.AscentFeet + rhs.AscentFeet,
                descentFeet: this.DescentFeet + rhs.DescentFeet,
                intermediateLocations: this.IntermediateLocations.Concat(this.EndLocation).Concat(rhs.IntermediateLocations).ToList());
        }

        public override int GetHashCode()
        {
            return HashCode.Create()
                .Add(this.StartLocation)
                .Add(this.EndLocation)
                .AddAll(this.IntermediateLocations)
                .Add(this.Direction)
                .Value;
        }

        public override bool Equals(object? obj)
        {
            return obj is WonderlandTrailConnection rhs &&
                this.StartLocation == rhs.StartLocation &&
                this.EndLocation == rhs.EndLocation &&
                Enumerables.SequenceEqual(this.IntermediateLocations, rhs.IntermediateLocations) &&
                this.Direction == rhs.Direction;
        }

        public override string ToString()
        {
            return $"{{{nameof(StartLocation)}:{StartLocation},{nameof(EndLocation)}:{EndLocation},{nameof(Direction)}:{Direction},{nameof(DistanceMiles)}:{DistanceMiles},{nameof(AscentFeet)}:{AscentFeet},{nameof(DescentFeet)}:{DescentFeet},{nameof(IntermediateLocations)}:{Enumerables.ToString(IntermediateLocations)}}}";
        }

        public bool Contains(WonderlandTrailLocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            return this.GetLocations().Contains(location);
        }

        public bool Contains(WonderlandTrailConnection rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return this.GetLocations().ContainsAll(rhs.GetLocations());
        }

        public bool IsLoop()
        {
            return this.StartLocation == this.EndLocation;
        }
    }
}
