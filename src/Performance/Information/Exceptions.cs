using System;
using System.Collections;

namespace MemoryAndPerformance.Performance.Information
{
    //Note: a common abuse of this is using exceptions for flow control such as:
    //      a .Parse with a catch and handling code instead of .TryParse with a conditional on the bool result

    public static class Exceptions
    {
        public static void TestPerformance(Action<SortedList, int> action)
        {
            var list = new SortedList();                                //this object only allows unique items (exceptions if you try to add a duplicate)
            for (var i = 0; i < 11; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    action(list, j);                                    //add 10 items to the list if it is the first time through
                }
            }
        }

        public static void AddWithCheckedInput(SortedList list, int i)
        {
            if (!list.Contains(i))
            {
                list.Add(i, i);
            }
        }

        public static void AddWithException(SortedList list, int i)
        {
            try
            {
                list.Add(i, i);
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
