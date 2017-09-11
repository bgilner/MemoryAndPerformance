using System;

namespace MemoryAndPerformance.Memory.Efficiency
{
    public class TestFinally
    {
        public bool ThrowException;
        private byte[] allocation = new byte[2 << 20];

        public static void Test(bool throwException)
        {
            Console.WriteLine($"\nthrowException = {throwException}, memory = {GC.GetTotalMemory(true) >> 20}mb");
            for (var i = 0; i < 5; i++)
            {
                new TestFinally { ThrowException = throwException };
                GC.Collect();
                Console.WriteLine("memory = {0}mb", GC.GetTotalMemory(false) >> 20);
            }
        }

        ~TestFinally()
        {
            // doing some important work which may be slow (like throwing an exception)
            try
            {
                if (ThrowException)
                {
                    throw new ApplicationException();
                }
            }
            catch (ApplicationException)
            {
            }
        }
    }
}
