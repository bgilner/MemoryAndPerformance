using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MemoryAndPerformance.Memory.Efficiency;
using MemoryAndPerformance.Memory.Information;
using MemoryAndPerformance.Memory.Leaks;
using MemoryAndPerformance.Performance.Information;

namespace MemoryAndPerformance.Testing
{
    public static class Tests
    {
        public static void RunAll()
        {
            GetAll().ForEach(TestHelper.ConsoleTest);
        }

        public static List<Action> GetAll()
        {
            return new List<Action>
            {
                TestLargeObjectHeap,    //Memory information
                TestStringAllocations,

                TestExceptions,         //Performance information
                TestRegexes,

                TestTimerLeaks,         //Memory leaks
                TestEventLeaks,

                TestSlowFinally,        //Memory efficiency
                TestTempVariable,
                TestEnumerationVsList,
                TestCapacity
            };
        }

        public static void TestLargeObjectHeap()
        {
            Allocations.TestLOH();
        }

        public static void TestStringAllocations()
        {
            Allocations.TestDynamicStrings();
        }

        private static void TestExceptions()
        {
            TestHelper.Time("Exceptions", () => Exceptions.TestPerformance(Exceptions.AddWithException));
            TestHelper.Time("Checked Inputs", () => Exceptions.TestPerformance(Exceptions.AddWithCheckedInput));
        }

        private static void TestRegexes()
        {
            Action<Action> GetTester(int iterations) => method => TestHelper.Time(method.Method.Name, () => RegexTester.Test(iterations, method));
            Action[] regexTests = { RegexTester.CompiledRegex, RegexTester.InstanceRegex, RegexTester.StaticRegex };

            TestHelper.Run(GetTester(500), regexTests);        //Note: getTester is curried - it returns a test action specific to an int
            TestHelper.Run(GetTester(1000), regexTests);
        }

        private static void TestTimerLeaks()
        {
            TestHelper.Run(TimerThreadLeak.Start, false, true);
        }

        private static void TestEventLeaks()
        {
            TestHelper.Run(EventLeaks.Test, EventLeakType.Unsubscribe, EventLeakType.ExplicitNull, EventLeakType.DontUnsubscribe);
        }

        private static void TestSlowFinally()
        {
            TestHelper.Run(TestFinally.Test, true, false);
        }

        private static void TestTempVariable()
        {
            TempVar.ReadFileTempVar();
            TempVar.ReadFileNoTempVar();
        }

        private static void TestEnumerationVsList()
        {
            TestHelper.Run<Func<IEnumerable<string>>>(EnumerationVsList.Test, EnumerationVsList.GetStrings, EnumerationVsList.GetStringList);
        }

        private static void TestCapacity()
        {
            TestHelper.Run(Capacity.Test, new StringBuilder(), new StringBuilder(Capacity.TEST_SIZE + 1));
            TestHelper.Run(Capacity.Test, new MemoryStream(), new MemoryStream(Capacity.TEST_SIZE + 1));
        }
    }
}
