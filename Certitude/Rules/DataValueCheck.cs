using System;
using Infrastructure.Resources;

namespace Certitude.Rules
{
    // checks if the value of the notification exceeds a given value
    class DataValueCheck : IRule
    {
        private readonly Single _value;

        public DataValueCheck(Single value)
        {
            _value = value;
        }

        public string Execute(string notification)
        {
            string sql = string.Format("SELECT DataValue FROM t_events WHERE TraceID = UNHEX('{0}') LIMIT 1", notification);
 
            string value = ResourceContainer.Database.ExecuteScalar("events", sql) as string;

            Single s;
            if (Single.TryParse(value, out s))
            {
                if (s >= _value)
                {
                    return GetType().Name + " failed";
                }   
            }

            return string.Empty;
        }
    }
}