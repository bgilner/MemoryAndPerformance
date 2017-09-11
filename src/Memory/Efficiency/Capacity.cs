using System;
using System.IO;
using System.Text;

namespace MemoryAndPerformance.Memory.Efficiency
{
    public static class Capacity
    {
        //NOTE: explicit capacities can be a performance enhancement as collections must reallocate many times as they grow
        public const int TEST_SIZE = 8 << 10;

        public static void Test(StringBuilder sb)
        {
            //Note that at 8k+ capacity, StringBuilder growth rate is less than double (better than a MemoryStream)
            var bigString = new string('X', TEST_SIZE);
            Console.WriteLine($"{sb.GetType()} initial capacity = {sb.Capacity >> 10}kb");
            sb.Append(bigString);
            sb.Append("X");
            Console.WriteLine($"{sb.GetType()} allocation +1 char capacity = {sb.Capacity >> 10}kb\n");
        }

        public static void Test(MemoryStream ms)
        {
            var bytes = new byte[TEST_SIZE];
            Console.WriteLine($"{ms.GetType()} initial capacity = {ms.Capacity >> 10}kb");
            ms.Write(bytes, 0, bytes.Length);
            ms.WriteByte(0);                    //<- this will double the allocation size
            Console.WriteLine($"{ms.GetType()} allocation +1 byte capacity = {ms.Capacity >> 10}kb\n");
            ms.Dispose();
        }
    }
}
