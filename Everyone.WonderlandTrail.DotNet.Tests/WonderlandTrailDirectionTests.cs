namespace Everyone
{
    public static class WonderlandTrailDirectionTests
    {
        public static void Test(TestRunner runner)
        {
            runner.TestGroup(typeof(WonderlandTrailDirection), () =>
            {
                runner.TestGroup("Reverse()", () =>
                {
                    void ReverseTest(WonderlandTrailDirection direction, WonderlandTrailDirection expected)
                    {
                        runner.Test($"with {direction}", (Test test) =>
                        {
                            test.AssertEqual(expected, direction.Reverse());
                        });
                    }

                    ReverseTest(WonderlandTrailDirection.Clockwise, WonderlandTrailDirection.CounterClockwise);
                    ReverseTest(WonderlandTrailDirection.CounterClockwise, WonderlandTrailDirection.Clockwise);
                });
            });
        }
    }
}
