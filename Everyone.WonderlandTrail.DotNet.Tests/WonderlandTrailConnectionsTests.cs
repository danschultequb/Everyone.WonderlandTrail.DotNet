using System;

namespace Everyone
{
    public static class WonderlandTrailConnectionsTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(WonderlandTrailConnections), () =>
            {
                runner.Test("Create()", (Test test) =>
                {
                    WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                    test.AssertNotNull(connections);
                    test.AssertEqual(0, connections.Count);
                    test.AssertEqual(new WonderlandTrailConnection[0], connections.GetConnections());
                });

                runner.TestGroup("AddConnection(WonderlandTrailConnection)", () =>
                {
                    runner.Test("with null", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        test.AssertThrows(new ArgumentNullException("connection"), () =>
                        {
                            connections.AddConnection(null!);
                        });
                        test.AssertEqual(new WonderlandTrailConnection[0], connections.GetConnections());
                    });

                    runner.Test("with non-existing", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        WonderlandTrailConnection connection = WonderlandTrailConnection.Create(
                            startLocation: CreateLocation(name: "A"),
                            endLocation: CreateLocation(name: "B"),
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);

                        connections.AddConnection(connection);
                        test.AssertEqual(new[] { connection }, connections.GetConnections());
                        test.AssertEqual(1, connections.Count);
                    });

                    runner.Test("with existing", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        WonderlandTrailConnection connection = WonderlandTrailConnection.Create(
                            startLocation: CreateLocation(name: "A"),
                            endLocation: CreateLocation(name: "B"),
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);
                        connections.AddConnection(connection);

                        connections.AddConnection(connection);
                        test.AssertEqual(new[] { connection }, connections.GetConnections());
                        test.AssertEqual(1, connections.Count);
                    });
                });

                runner.TestGroup("ReverseDirection()", () =>
                {
                    runner.Test("with no connections", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        
                        WonderlandTrailConnections reversedConnections = connections.ReverseDirection();
                        test.AssertNotNull(reversedConnections);
                        test.AssertEqual(new WonderlandTrailConnection[0], reversedConnections.GetConnections());
                    });

                    runner.Test("with one connection", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        connections.AddConnection(WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3));
                        
                        WonderlandTrailConnections reversedConnections = connections.ReverseDirection();
                        test.AssertNotNull(reversedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                WonderlandTrailConnection.Create(
                                    startLocation: bLocation,
                                    endLocation: aLocation,
                                    direction: WonderlandTrailDirection.CounterClockwise,
                                    distanceMiles: 1,
                                    ascentFeet: 3,
                                    descentFeet: 2),
                            },
                            reversedConnections.GetConnections());
                    });

                    runner.Test("with two connections", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        WonderlandTrailLocation cLocation = CreateLocation(name: "c");
                        connections.AddConnection(WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3));
                        connections.AddConnection(WonderlandTrailConnection.Create(
                            startLocation: bLocation,
                            endLocation: cLocation,
                            distanceMiles: 4,
                            ascentFeet: 5,
                            descentFeet: 6));

                        WonderlandTrailConnections reversedConnections = connections.ReverseDirection();
                        test.AssertNotNull(reversedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                WonderlandTrailConnection.Create(
                                    startLocation: bLocation,
                                    endLocation: aLocation,
                                    direction: WonderlandTrailDirection.CounterClockwise,
                                    distanceMiles: 1,
                                    ascentFeet: 3,
                                    descentFeet: 2),
                                WonderlandTrailConnection.Create(
                                    startLocation: cLocation,
                                    endLocation: bLocation,
                                    direction: WonderlandTrailDirection.CounterClockwise,
                                    distanceMiles: 4,
                                    ascentFeet: 6,
                                    descentFeet: 5),
                            },
                            reversedConnections.GetConnections());
                    });
                });

                runner.TestGroup("ExpandConnections()", () =>
                {
                    runner.Test("with no connections", (Test test) =>
                    {
                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        
                        WonderlandTrailConnections expandedConnections = connections.ExpandConnections();
                        test.AssertNotNull(expandedConnections);
                        test.AssertNotSame(connections, expandedConnections);
                        test.AssertEqual(
                            new WonderlandTrailConnection[0],
                            expandedConnections.GetConnections());

                        test.AssertEqual(
                            new WonderlandTrailConnection[0],
                            connections.GetConnections());
                    });

                    runner.Test("with one connection", (Test test) =>
                    {
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        WonderlandTrailConnection abConnection = WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);

                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        connections.AddConnection(abConnection);

                        WonderlandTrailConnections expandedConnections = connections.ExpandConnections();
                        test.AssertNotNull(expandedConnections);
                        test.AssertNotSame(connections, expandedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                abConnection,
                            },
                            expandedConnections.GetConnections());

                        test.AssertEqual(
                            new[]
                            {
                                abConnection,
                            },
                            connections.GetConnections());
                    });

                    runner.Test("with two unrelated connections", (Test test) =>
                    {
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        WonderlandTrailLocation cLocation = CreateLocation(name: "c");
                        WonderlandTrailLocation dLocation = CreateLocation(name: "d");
                        WonderlandTrailConnection abConnection = WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);
                        WonderlandTrailConnection cdConnection = WonderlandTrailConnection.Create(
                            startLocation: cLocation,
                            endLocation: dLocation,
                            distanceMiles: 4,
                            ascentFeet: 5,
                            descentFeet: 6);

                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        connections.AddConnection(abConnection);
                        connections.AddConnection(cdConnection);

                        WonderlandTrailConnections expandedConnections = connections.ExpandConnections();
                        test.AssertNotNull(expandedConnections);
                        test.AssertNotSame(connections, expandedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                cdConnection,
                                abConnection,
                            },
                            expandedConnections.GetConnections());

                        test.AssertEqual(
                            new[]
                            {
                                abConnection,
                                cdConnection,
                            },
                            connections.GetConnections());
                    });

                    runner.Test("with two connections that form a line", (Test test) =>
                    {
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        WonderlandTrailLocation cLocation = CreateLocation(name: "c");
                        WonderlandTrailConnection abConnection = WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);
                        WonderlandTrailConnection bcConnection = WonderlandTrailConnection.Create(
                            startLocation: bLocation,
                            endLocation: cLocation,
                            distanceMiles: 4,
                            ascentFeet: 5,
                            descentFeet: 6);

                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        connections.AddConnection(abConnection);
                        connections.AddConnection(bcConnection);

                        WonderlandTrailConnections expandedConnections = connections.ExpandConnections();
                        test.AssertNotNull(expandedConnections);
                        test.AssertNotSame(connections, expandedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                bcConnection,
                                abConnection,
                                WonderlandTrailConnection.Create(
                                    startLocation: aLocation,
                                    endLocation: cLocation,
                                    distanceMiles: 5,
                                    ascentFeet: 7,
                                    descentFeet: 9,
                                    intermediateLocations: new[] { bLocation }),
                            },
                            expandedConnections.GetConnections());

                        test.AssertEqual(
                            new[]
                            {
                                abConnection,
                                bcConnection,
                            },
                            connections.GetConnections());
                    });

                    runner.Test("with two connections that form a loop", (Test test) =>
                    {
                        WonderlandTrailLocation aLocation = CreateLocation(name: "a");
                        WonderlandTrailLocation bLocation = CreateLocation(name: "b");
                        WonderlandTrailConnection abConnection = WonderlandTrailConnection.Create(
                            startLocation: aLocation,
                            endLocation: bLocation,
                            distanceMiles: 1,
                            ascentFeet: 2,
                            descentFeet: 3);
                        WonderlandTrailConnection baConnection = WonderlandTrailConnection.Create(
                            startLocation: bLocation,
                            endLocation: aLocation,
                            distanceMiles: 4,
                            ascentFeet: 5,
                            descentFeet: 6);

                        WonderlandTrailConnections connections = WonderlandTrailConnections.Create();
                        connections.AddConnection(abConnection);
                        connections.AddConnection(baConnection);

                        WonderlandTrailConnections expandedConnections = connections.ExpandConnections();
                        test.AssertNotNull(expandedConnections);
                        test.AssertNotSame(connections, expandedConnections);
                        test.AssertEqual(
                            new[]
                            {
                                baConnection,
                                WonderlandTrailConnection.Create(
                                    startLocation: bLocation,
                                    endLocation: bLocation,
                                    distanceMiles: 5,
                                    ascentFeet: 7,
                                    descentFeet: 9,
                                    intermediateLocations: new[] { aLocation }),
                                abConnection,
                                WonderlandTrailConnection.Create(
                                    startLocation: aLocation,
                                    endLocation: aLocation,
                                    distanceMiles: 5,
                                    ascentFeet: 7,
                                    descentFeet: 9,
                                    intermediateLocations: new[] { bLocation }),
                            },
                            expandedConnections.GetConnections());

                        test.AssertEqual(
                            new[]
                            {
                                abConnection,
                                baConnection,
                            },
                            connections.GetConnections());
                    });
                });
            });
        }

        private static WonderlandTrailLocation CreateLocation(string name)
        {
            return WonderlandTrailLocation.Create(name: name, trailhead: false, foodCacheStorage: false, divisionId: "", groupSiteDivisionId: "");
        }
    }
}
