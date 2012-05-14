using System;
using System.Diagnostics;
using Divvy.Platform;

namespace Divvy.Drone.Execution
{
    /// <summary>
    /// wraps the execution of tasks
    /// </summary>
    class TaskExecutor
    {
        private readonly BaseTask _task;

        public TaskExecutor(BaseTask task)
        {
            _task = task;
        }

        public string Execute()
        {
            try
            {
                if (_task != null)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string result = _task.Execute(System.Threading.Thread.CurrentThread.Name);
                    Console.WriteLine("execution time: {0}", stopwatch.ElapsedMilliseconds);
                    return result;
                }
            }
            catch (Exception)
            {
            }
            return "failure";
        }
    }
}
