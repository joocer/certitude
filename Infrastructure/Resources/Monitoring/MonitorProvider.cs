using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Infrastructure.Resources.Monitoring
{
    class MonitorProvider : IMonitorService
    {
        //TODO: this class has repeated code, this should be cleaned up

        private string _category;
        private bool _enabled;
        private readonly Dictionary<string, PerformanceCounter> _counterCache = new Dictionary<string,PerformanceCounter>();

        private void CreateCounter(string counter)
        {
            // Create the counter and set the properties.
            CounterCreationData counterCreationData = new CounterCreationData
                                                          {
                                                              CounterName = counter,
                                                              CounterHelp = counter,
                                                              CounterType = PerformanceCounterType.NumberOfItems64
                                                          };

            CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection
                                                                              {counterCreationData};

            // Create the category and pass the collection to it.
            try
            {
                PerformanceCounterCategory.Create(_category,
                                                  "Application Counters",
                                                  PerformanceCounterCategoryType.SingleInstance,
                                                  counterCreationDataCollection);
            }
            catch (Exception)
            {
                _enabled = false;
            }
        }

        public void IncrementCounter(string counter)
        {
            if (String.IsNullOrEmpty(_category))
            {
                _category = ResourceContainer.Configuration.ReadValue("diagnostics", "performance-counters-category");
                _enabled = (ResourceContainer.Configuration.ReadValue("diagnostics", "performance-counters-enabled").ToLower() == "true");
            }

            // if we're not enabled, just exit
            if (!_enabled) { return; }

            // checked if the counter is cached
            if (!_counterCache.ContainsKey(counter))
            {
                // create the counter if it didn't exist
                if (!PerformanceCounterCategory.Exists(_category) || !PerformanceCounterCategory.CounterExists(counter, _category))
                {
                    CreateCounter(counter);
                }

                if (!_enabled) { return; }

                // cache the counter
                PerformanceCounter performanceCounter = new PerformanceCounter(_category, counter, false);
                _counterCache.Add(counter, performanceCounter);
            }

            // increment the counter
            _counterCache[counter].Increment();
        }

        public void IncrementCounterBy(string counter, int amount)
        {
            if (String.IsNullOrEmpty(_category))
            {
                _category = ResourceContainer.Configuration.ReadValue("diagnostics", "performance-counters-category");
                _enabled = (ResourceContainer.Configuration.ReadValue("diagnostics", "performance-counters-enabled").ToLower() == "true");
            }

            // if we're not enabled, just exit
            if (!_enabled) { return; }

            // checked if the counter is cached
            if (!_counterCache.ContainsKey(counter))
            {
                // create the counter if it didn't exist
                if (!PerformanceCounterCategory.Exists(_category) || !PerformanceCounterCategory.CounterExists(counter, _category))
                {
                    CreateCounter(counter);
                }

                if (!_enabled) { return; }

                // cache the counter
                PerformanceCounter performanceCounter = new PerformanceCounter(_category, counter, false);
                _counterCache.Add(counter, performanceCounter);
            }

            // increment the counter
            _counterCache[counter].IncrementBy(amount);
        }
    }
}
