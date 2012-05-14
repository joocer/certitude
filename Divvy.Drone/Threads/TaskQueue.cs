using System.Threading;

/******************************************************************************
 * 
 * This is a manager for setting and forgetting asynchronous tasks
 * 
 * ***************************************************************************/

namespace Divvy.Drone.Threads
{
    public static class TaskQueue
    {
        private static readonly object Lock = new object();
        private static readonly BackgroundTaskQueueProvider Queue = CreateQueue();

        public static void Enqueue(string task)
        {
            Queue.Enqueue(task);
        }

        private static BackgroundTaskQueueProvider CreateQueue()
        {
            lock (Lock)
            {
                BackgroundTaskQueueProvider ret = new BackgroundTaskQueueProvider(ThreadPriority.Lowest);
                ret.OnStateChanged += StateChanged;
                ret.OnStateChanged += StateChanged;
                return ret;
            }
        }

        private static void StateChanged(bool busy)
        {
            if (OnStateChanged != null) { OnStateChanged(busy); }
        }

        public static bool Busy
        {
            get { return !Queue.AllAsleep(); }
        }

        public delegate void ChangedEventHandler(bool busy);
        public static event ChangedEventHandler OnStateChanged;
    }
}
