using System;
using System.IO;

namespace MemoryAndPerformance.Memory.Efficiency
{
    public static class TempVar
    {
        private static readonly string _bigFile;

        static TempVar()
        {
            _bigFile = Path.GetTempFileName();
            File.WriteAllBytes(_bigFile, new byte[3 << 20]);
            AppDomain.CurrentDomain.ProcessExit += (o, e) => File.Delete(_bigFile);
        }

        public static void ReadFileTempVar()
        {
            Console.WriteLine($"In ReadFileTempVar,   start memory = {GC.GetTotalMemory(true) >> 20}mb");
            
            var reader = new StreamReader(_bigFile);
            var contents = reader.ReadToEnd();          //temp variable allocated here
            var length = contents.Length;               //contents has a longer lifetime than needed, .NET doesn't know that it is out of scope

            //.. code processes with diminished memory ...
            Console.WriteLine($"In ReadFileTempVar,     end memory = {GC.GetTotalMemory(true) >> 20}mb\n");
        }

        public static void ReadFileNoTempVar()
        {
            Console.WriteLine($"In ReadFileNoTempVar, start memory = {GC.GetTotalMemory(true) >> 20}mb");
            
            var reader = new StreamReader(_bigFile);
            var length = reader.ReadToEnd().Length;     //<- .NET is smart enough to know that reader.ReadToEnd() immediately falls out of scope

            //.. code processes with full memory ...
            Console.WriteLine($"In ReadFileNoTempVar,   end memory = {GC.GetTotalMemory(true) >> 20}mb\n");
        }
    }
}
