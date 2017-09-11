using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryAndPerformance.Memory.Efficiency
{
    public static class EnumerationVsList
    {
        //Note: a common memory optimization is to refactor a List into an IEnumerable if all usages are only for enumeration
        //  Caveat 1: It is inefficient to have multiple enumerations of the list in the same scope and they won't be equal objects
        //  Caveat 2: Ensure the enumerations isn't accessed while it is being modified elsewhere (collection was modified execption).

        public static void Test(Func<IEnumerable<string>> enumeration)
        {
            foreach (var unused in enumeration())
            {
                Console.WriteLine($"Inside {enumeration.Method.Name}, used memory = {GC.GetTotalMemory(true) >> 20}mb");
            }
        }

        public static IEnumerable<string> GetStringList()
        {
            return new List<string>(GetStrings());
        }
        
        public static IEnumerable<string> GetStrings()
        {
            return Enumerable.Range(0, 3).Select(x => new string((char)x, 1 << 20));
        }
    }
}
