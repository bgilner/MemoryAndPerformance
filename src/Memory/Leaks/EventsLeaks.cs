using System;

namespace MemoryAndPerformance.Memory.Leaks
{
    //Note: a good practice is to have subscribers implement IDisposable wherein events are unsubscriber.
    //if events aren't unsubscribed, .Net cannot know to GC the publisher without this explicit signal

    public enum EventLeakType { Unsubscribe, DontUnsubscribe, ExplicitNull }

    public static class EventLeaks
    {
        public static void Test(EventLeakType type)
        {
            GarbageCollect();
            Console.WriteLine($"\nTest type = {type}.");

            var publisher = new Publisher();
            var subscriber = new Subscriber();
            publisher.MyEvent += subscriber.NoopHandler;
            if (type == EventLeakType.Unsubscribe)
            {
                publisher.MyEvent -= subscriber.NoopHandler;
            }
            else if (type == EventLeakType.ExplicitNull)
            {
                publisher = null;
            }

            Console.WriteLine("\tPublisher should be able to be GCed (finalizer called)");
            GarbageCollect();
        }

        private static void GarbageCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public class Publisher
    {
        public event EventHandler MyEvent;

        ~Publisher()
        {
            Console.WriteLine("\tPublisher finalizer called");
        }
    }

    public class Subscriber
    {
        public readonly EventHandler NoopHandler = (s, a) => {};
    }
}