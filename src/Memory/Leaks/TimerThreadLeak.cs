using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MemoryAndPerformance.Memory.Leaks
{
    /* Note: one example of this is explained from MSDN:
     * 
     * If a System.Timers.Timer is used in a WPF application, it is worth noting that the System.Timers.Timer runs on a
     * different thread then the user interface (UI) thread. In order to access objects on the user interface (UI) thread,
     * it is necessary to post the operation onto the Dispatcher of the user interface (UI) thread using Invoke or BeginInvoke. 
     * 
     * Reasons for using a DispatcherTimer opposed to a System.Timers.Timer are that the DispatcherTimer runs on the same
     * thread as the Dispatcher and a DispatcherPriority can be set on the DispatcherTimer.
    */

    public static class TimerThreadLeak
    {
        private static bool _timerHandlerSleep;

        public static void Start(bool timerHandlerSleep)
        {
            Console.WriteLine($"Start, sleep = {timerHandlerSleep}");
            _timerHandlerSleep = timerHandlerSleep;
            var timer = new Timer(100);
            timer.Elapsed += timer_Elapsed;

            timer.Start();
            Thread.Sleep(3000);
            timer.Stop();

            Console.WriteLine("Stop");
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var allocation = new byte[5 << 20];     //to simulate running code that allocates memory
            Console.WriteLine($"Memory = {GC.GetTotalMemory(true) >> 20}mb, Thread count = {Process.GetCurrentProcess().Threads.Count}");
            if (_timerHandlerSleep)
            {
                Thread.Sleep(3000);                 //this could be any long-running operation (longer than the thread tick)
            }
        }
    }
}
