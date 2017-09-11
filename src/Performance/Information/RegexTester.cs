using System;
using System.Text.RegularExpressions;

namespace MemoryAndPerformance.Performance.Information
{
    public static class RegexTester
    {
        //Note: Don't carelessly compile everything. It can harm performance since .Net has a fixed cache.
        //      See: https://msdn.microsoft.com/en-us/library/system.text.regularexpressions.regex.cachesize(v=vs.110).aspx

        /*
         * Note: a common optimization is:
         * 
         * before (regex overhead per list item):
         *      var matches = list.Where(item => Regex.IsMatch(item, regexFilter));
         *      
         * after (regex overhead once total):
         *      var filters = new Regex(regexFilter);
         *      var matches = list.Where(item => filters.IsMatch(item));
        */

        public const string PATTERN = "s.+ing";
        public const string SOURCE = "This is a simple string for Regex.";
        private static readonly Regex _compiledRegex = new Regex(PATTERN, RegexOptions.Compiled);
        private static readonly Regex _instanceRegex = new Regex(PATTERN);

        public static void Test(int iterations, Action action)
        {
            Console.Write($"{iterations} Iterations\t");
            while (iterations-- > 0)
            {
                action();
            }
        }

        private static void RunRegexTests(Regex regex)
        {
            regex.Match(SOURCE);
            regex.Split(SOURCE);
            regex.Replace(SOURCE, "test");
        }

        public static void CompiledRegex()
        {
            RunRegexTests(_compiledRegex);
        }

        public static void InstanceRegex()
        {
            RunRegexTests(_instanceRegex);
        }

        public static void StaticRegex()
        {
            Regex.Match(SOURCE, PATTERN);
            Regex.Split(SOURCE, PATTERN);
            Regex.Replace(SOURCE, PATTERN, "test");
        }
    }
}
