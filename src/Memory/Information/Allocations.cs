using System;
using System.Text;

namespace MemoryAndPerformance.Memory.Information
{
    public static class Allocations
    {
        public static void TestLOH()
        {
            //NOTE: Around 85k for all except double[]
            void PrintGeneration(string op, Array obj) => Console.WriteLine($"GC Generation {GC.GetGeneration(obj)}: {op}{obj.Length} {obj.GetType()}");

            PrintGeneration("<=", new byte[84975]);
            PrintGeneration(">=", new byte[84976]);

            PrintGeneration("<=", new string('X', 42487).ToCharArray());    //Note: string and char arrays function identically
            PrintGeneration(">=", new string('X', 42488).ToCharArray());

            /* The expected tipping point into the LOH occurs at (85000 / 8) = 10625. 
             * Need 12 bytes for object overhead, 
             * nearest is 16 (2*8) bytes so effectively 10623 doubles" */
            PrintGeneration("<= ", new double[10621]);
            PrintGeneration(">= ", new double[10622]);
        }

        public static void TestDynamicStrings()
        {
            //Note: dynamically allocated strings always create a new allocation (they don't use .Net string intern pool)
            void CompareString(string desc, object s) => Console.WriteLine($"{desc} string, reference equal = {ReferenceEquals("Hi", s)}");

            CompareString("Dynamic", new StringBuilder("Hi").ToString());
            CompareString("Literal", "Hi");
        }
    }
}

