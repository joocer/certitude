using System;
using Divvy.Drone.Threads;
using Infrastructure.Resources;

namespace Divvy.Drone
{
    class EntryPoint
    {
        static void Main(string[] args)
        {

            Console.WriteLine(ResourceContainer.Configuration.ReadValue("encryption", "key"));

            ThreadManager threadManager = new ThreadManager();
            threadManager.Initialize();
            threadManager.Start();
            Console.ReadLine();
            threadManager.Stop();
        }
    }
}