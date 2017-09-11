using System;
using System.Diagnostics;

namespace MemoryAndPerformance.Testing
{
    public static class TestHelper
    {
        public static void Run<T>(Action<T> test, params T[] parameters)
        {
            Array.ForEach(parameters, test);
        }

        public static void ConsoleTest(Action testAction)
        {
            Console.WriteLine($"{testAction.Method.Name}:\n");
            testAction();
            Console.WriteLine("\npress a key to continue ...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Time(string description, Action testAction)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            testAction();
            stopwatch.Stop();
            Console.WriteLine($"{description}\t{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
