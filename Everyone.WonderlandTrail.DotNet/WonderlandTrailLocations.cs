using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Everyone
{
    public static class WonderlandTrailLocations
    {
        public static WonderlandTrailLocation GraniteCreek { get; } = WonderlandTrailLocation.Create(
            name: "Granite Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170009",
            groupSiteDivisionId: "46753170010");

        public static WonderlandTrailLocation SunriseCamp { get; } = WonderlandTrailLocation.Create(
            name: "Sunrise Camp",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170058",
            groupSiteDivisionId: "46753170059");

        public static WonderlandTrailLocation SunriseVisitorCenter { get; } = WonderlandTrailLocation.Create(
            name: "Sunrise Visitor Center",
            trailhead: true,
            foodCacheStorage: true,
            divisionId: "",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation WhiteRiver { get; } = WonderlandTrailLocation.Create(
            name: "White River",
            trailhead: true,
            foodCacheStorage: true,
            divisionId: "46753170066",
            groupSiteDivisionId: "46753170067");

        public static WonderlandTrailLocation FryingPanCreek { get; } = WonderlandTrailLocation.Create(
            name: "Fryingpan Creek",
            trailhead: true,
            foodCacheStorage: false,
            divisionId: "",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation Summerland { get; } = WonderlandTrailLocation.Create(
            name: "Summerland",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170056",
            groupSiteDivisionId: "46753170057");

        public static WonderlandTrailLocation IndianBar { get; } = WonderlandTrailLocation.Create(
            name: "Indian Bar",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170046",
            groupSiteDivisionId: "46753170047");

        public static WonderlandTrailLocation NickelCreek { get; } = WonderlandTrailLocation.Create(
            name: "Nickel Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170051",
            groupSiteDivisionId: "46753170052");

        
        public static WonderlandTrailLocation BoxCanyon { get; } = WonderlandTrailLocation.Create(
            name: "Box Canyon",
            trailhead: true,
            foodCacheStorage: false,
            divisionId: "",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation MapleCreek { get; } = WonderlandTrailLocation.Create(
            name: "Maple Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170027",
            groupSiteDivisionId: "46753170028");

        public static WonderlandTrailLocation ReflectionLakes { get; } = WonderlandTrailLocation.Create(
            name: "Reflection Lakes",
            trailhead: true,
            foodCacheStorage: false,
            divisionId: "",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation ParadiseRiver { get; } = WonderlandTrailLocation.Create(
            name: "Paradise River",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170031",
            groupSiteDivisionId: "46753170032");

        public static WonderlandTrailLocation Longmire { get; } = WonderlandTrailLocation.Create(
            name: "Longmire",
            trailhead: true,
            foodCacheStorage: true,
            divisionId: "",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation PyramidCreek { get; } = WonderlandTrailLocation.Create(
            name: "Pyramid Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170033",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation DevilsDream { get; } = WonderlandTrailLocation.Create(
            name: "Devil's Dream",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170040",
            groupSiteDivisionId: "46753170041");

        public static WonderlandTrailLocation SouthPuyallupRiver { get; } = WonderlandTrailLocation.Create(
            name: "South Puyallup River",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170035",
            groupSiteDivisionId: "46753170036");

        public static WonderlandTrailLocation KlapatchePark { get; } = WonderlandTrailLocation.Create(
            name: "Klapatche Park",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170024",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation NorthPuyallupRiver { get; } = WonderlandTrailLocation.Create(
            name: "North Puyallup River",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170029",
            groupSiteDivisionId: "46753170030");

        public static WonderlandTrailLocation GoldenLakes { get; } = WonderlandTrailLocation.Create(
            name: "Golden Lakes",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170022",
            groupSiteDivisionId: "46753170023");

        public static WonderlandTrailLocation SouthMowichRiver { get; } = WonderlandTrailLocation.Create(
            name: "South Mowich River",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170019",
            groupSiteDivisionId: "46753170020");

        public static WonderlandTrailLocation MowichLake { get; } = WonderlandTrailLocation.Create(
            name: "Mowich Lake",
            trailhead: true,
            foodCacheStorage: true,
            divisionId: "46753170015",
            groupSiteDivisionId: "46753170016");

        public static WonderlandTrailLocation EaglesRoost { get; } = WonderlandTrailLocation.Create(
            name: "Eagle's Roost",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170006",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation CataractValley { get; } = WonderlandTrailLocation.Create(
            name: "Cataract Valley",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170003",
            groupSiteDivisionId: "46753170004");

        public static WonderlandTrailLocation IpsutCreek { get; } = WonderlandTrailLocation.Create(
            name: "Ipsut Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170011",
            groupSiteDivisionId: "46753170012");

        public static WonderlandTrailLocation CarbonRiver { get; } = WonderlandTrailLocation.Create(
            name: "Carbon River",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170001",
            groupSiteDivisionId: "46753170002");

        public static WonderlandTrailLocation DickCreek { get; } = WonderlandTrailLocation.Create(
            name: "Dick Creek",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170005",
            groupSiteDivisionId: "");

        public static WonderlandTrailLocation MysticLake { get; } = WonderlandTrailLocation.Create(
            name: "Mystic Lake",
            trailhead: false,
            foodCacheStorage: false,
            divisionId: "46753170017",
            groupSiteDivisionId: "46753170018");


        public static IEnumerable<WonderlandTrailLocation> GetLocations()
        {
            Type type = typeof(WonderlandTrailLocations);
            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
            return propertyInfos
                .Where(propertyInfo => propertyInfo.PropertyType == typeof(WonderlandTrailLocation))
                .Select(propertyInfo => (WonderlandTrailLocation)propertyInfo.GetValue(obj: null)!);
        }

        public static IEnumerable<WonderlandTrailLocation> GetTrailheads()
        {
            return WonderlandTrailLocations.GetLocations()
                .Where(l => l.Trailhead);
        }
    }
}
