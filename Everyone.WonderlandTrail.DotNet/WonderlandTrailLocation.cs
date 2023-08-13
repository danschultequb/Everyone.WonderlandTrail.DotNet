using System;

namespace Everyone
{
    public class WonderlandTrailLocation
    {
        private WonderlandTrailLocation(string name, bool trailhead, bool foodCacheStorage, string divisionId, string groupSiteDivisionId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} cannot be null or empty.", nameof(name));
            }

            this.Name = name;
            this.Trailhead = trailhead;
            this.FoodCacheStorage = foodCacheStorage;
            this.DivisionId = divisionId ?? throw new ArgumentNullException(nameof(divisionId));
            this.GroupSiteDivisionId = groupSiteDivisionId ?? throw new ArgumentNullException(nameof(groupSiteDivisionId));
        }

        public static WonderlandTrailLocation Create(string name, bool trailhead, bool foodCacheStorage, string divisionId, string groupSiteDivisionId)
        {
            return new WonderlandTrailLocation(
                name: name,
                trailhead: trailhead,
                foodCacheStorage: foodCacheStorage,
                divisionId: divisionId,
                groupSiteDivisionId: groupSiteDivisionId);
        }

        public string Name { get; }

        public bool Trailhead { get; }

        public bool FoodCacheStorage { get; }

        public string DivisionId { get; }

        public string GroupSiteDivisionId { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
