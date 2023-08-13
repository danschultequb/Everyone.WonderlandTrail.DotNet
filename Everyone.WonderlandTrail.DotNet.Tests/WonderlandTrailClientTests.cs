using System;
using System.Collections.Generic;

namespace Everyone
{
    public static class WonderlandTrailClientTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(WonderlandTrailClient), () =>
            {
                runner.Test("Create()", (Test test) =>
                {
                    WonderlandTrailClient client = WonderlandTrailClient.Create();
                    test.AssertNotNull(client);
                });

                runner.TestGroup($"Create({nameof(RecreationDotGovClient)})", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        test.AssertThrows(new ArgumentNullException("client"), () =>
                        {
                            WonderlandTrailClient.Create(null!);
                        });
                    });

                    runner.Test("with non-null", (Test test) =>
                    {
                        WonderlandTrailClient client = WonderlandTrailClient.Create(RecreationDotGovClient.Create());
                        test.AssertNotNull(client);
                    });
                });

                runner.TestGroup("GetAvailabilityAsync(int,int,bool,bool,bool)", () =>
                {
                    runner.Test($"with {Language.AndList(8, 2023)}", (Test test) =>
                    {
                        WonderlandTrailClient client = WonderlandTrailClient.Create();
                        WonderlandTrailAvailability availability = client.GetAvailabilityAsync(
                            month: 8,
                            year: 2023,
                            allowWalkupPermits: true,
                            allowIndividualSites: true,
                            allowGroupSites: false).Await();
                        test.AssertNotNull(availability);
                    });
                });

                runner.Test("FindItinerariesAsync()", (Test test) =>
                {
                    WonderlandTrailClient client = WonderlandTrailClient.Create();

                    IEnumerable<WonderlandTrailItinerary> itineraries = client.FindItinerariesAsync(
                        startDay: new DateTime(month: 8, day: 15, year: 2023),
                        direction: WonderlandTrailDirection.Clockwise,
                        maximumDayDistanceMiles: 15,
                        maximumItineraryDays: 4,
                        allowWalkupPermits: true,
                        allowIndividualSites: true,
                        allowGroupSites: true,
                        startLocation: WonderlandTrailLocations.SunriseCamp,
                        endLocation: WonderlandTrailLocations.Longmire,
                        campsitesToAvoid: new WonderlandTrailLocation[]
                        {
                            // WonderlandTrailLocations.DevilsDream,
                            //WonderlandTrailLocations.MowichLake,
                        }).Await();
                    test.AssertNotNull(itineraries);
                });
            });
        }
    }
}