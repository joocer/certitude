using System;
using System.Collections.Generic;
using Infrastructure.Resources;
using Certitude.Services;

namespace Certitude.Rules.Aggregators
{
    /// <summary>
    /// A base class with default implementations of common tasks to perform event aggregations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EventAggregator<T>
    {
        // parameters
        public DateTime StartTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; set; }
        public String EventType { get; set; }
        public String DetectedBy { get; set; }
        public String SubjectID { get; set; }
        public String ClientID { get; set; }
        public string Command { get; set; }

        abstract public T Calculate();

        protected string RetrieveScalar()
        {
            byte[] scalar = ResourceContainer.Database.ExecuteScalar("events",
                                                     Command,
                                                     ResourceContainer.Clock.TimeStamp(StartTimeStamp),
                                                     ResourceContainer.Clock.TimeStamp(EndTimeStamp),
                                                     EventType,
                                                     DetectedBy,
                                                     SubjectID,
                                                     ClientID) as byte[];
            return scalar.AsString();
        }
    }
}
