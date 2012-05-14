using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Divvy.Drone.Execution;
using Divvy.Platform;

/******************************************************************************
 * 
 * This is a manager for setting and forgetting asynchronous tasks
 * 
 * ----------------------------------------------------------------------------
 * 03-FEB-2011 - JJ
 * ----------------------------------------------------------------------------
 * + rewritten using Sleep and Interrupt - 04-SEP-2011
 * + major changes to simplify
 * ***************************************************************************/

namespace Divvy.Drone.Threads
{
    public class BackgroundTaskQueueProvider : IDisposable
    {
        private readonly List<string> _queue;
        private readonly Dictionary<string, Thread> _threadPool = new Dictionary<string, Thread>();
        private readonly object _obj = new object();
        private readonly int _maximumThreads;
        private readonly Dictionary<string, bool> _threadStates = new Dictionary<string, bool>(); 

        public BackgroundTaskQueueProvider(ThreadPriority priority)
        {
            // set up the task queue 
            _queue = new List<string>();

            // spin up the threads - two per logical CPU
            _maximumThreads = Environment.ProcessorCount * 2;
#if DEBUG
            // if we're debugging, multithreading is a pain in the ass
            _maximumThreads = 1;
#endif
            
            for (int i = 0; i < _maximumThreads; i++)
            {
                _threadStates.Add(i.ToString(), false);
            }

            for (int i = 0; i < _maximumThreads; i++)
            {
                Thread thread = new Thread(Processor)
                                     {
                                         Priority = priority, 
                                         IsBackground = false, 
                                         Name = i.ToString(CultureInfo.InvariantCulture)
                                     };
                thread.Start();
                _threadPool.Add(i.ToString(), thread);
            }
        }

        public void Enqueue(string task)
        {
            lock (_obj)
            {
                _queue.Add(task);

                // if we have a spare thread, spin one up
                for (int i = 0; i < _maximumThreads; i++)
                {
                    string threadId = i.ToString();
                    if (!_threadStates[threadId] & Sleeping(threadId))
                    {
                        if (AllAsleep() && OnStateChanged != null)
                        {
                            OnStateChanged(true);
                        }
                        _threadStates[threadId] = true;
                        _threadPool[threadId].Interrupt();
                        break;
                    }
                }
            }
        }

        private void Processor()
        {
            string threadId = Thread.CurrentThread.Name;

            while (true)
            {
                if (_queue.Count > 0)
                {
                    try
                    {
                        Console.WriteLine("Thread {0} busy", threadId);
                        string source = Dequeue();

                        BaseTask task1 = TaskCompiler.Compile(source);
                        TaskExecutor executor = new TaskExecutor(task1);
                        Console.WriteLine(executor.Execute());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Thread {0} failed whilst running - {1}", threadId, exception.Message);
                    }
                    Console.WriteLine(String.Format("Thread {0} finished task. {1} jobs pending", threadId, _queue.Count));
                }
                else
                {
                    try
                    {
                        if (_threadStates[threadId])
                        {
                            Console.WriteLine("Thread {0} is being suspended", threadId);
                            _threadStates[threadId] = false;
                            if (AllAsleep() && OnStateChanged != null)
                            {
                                OnStateChanged(false);
                            }
                        }
                        Thread.Sleep(Timeout.Infinite);
                    }
                    catch (ThreadInterruptedException)
                    {
                        Console.WriteLine("Thread {0} awakened", threadId);
                    }
                }
            }
        }

        // kill the threads
        public void Dispose()
        {
            for (int i = 0; i < _maximumThreads; i++)
            {
                _threadPool[i.ToString()].Abort();
            }
        }

        private bool Sleeping(string threadID)
        {
            ThreadState ts = _threadPool[threadID].ThreadState;
            return ((ts & ThreadState.WaitSleepJoin) == ThreadState.WaitSleepJoin);
        }

        public bool AllAsleep()
        {
            return _threadStates.Values.All(a => !a);
        }

        private string Dequeue()
        {
            string source;
            lock (_obj)
            {
                source = _queue[0];
                _queue.RemoveAt(0);
            }
            return source;
        }

        public delegate void ChangedEventHandler(bool busy);
        public event ChangedEventHandler OnStateChanged;
    }
}