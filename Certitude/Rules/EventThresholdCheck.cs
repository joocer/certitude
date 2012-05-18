using System;
using Certitude.Services;
using Infrastructure.Resources;

namespace Certitude.Rules
{
    // checks if the value of the notification exceeds a given value
    class SubjectThresholdCheck : Rule
    {
        protected override RuleResult DoService(string notification)
        {
            RuleResult ruleResult = new RuleResult(GetType().Name);

            Single thresholdAmount = Single.Parse(GetParameter("subject-threshold-value"));
            int thresholdHours = int.Parse(GetParameter("subject-threshold-days"));

            string sql = string.Format("SELECT DataValue FROM t_events WHERE TraceID = UNHEX('{0}') LIMIT 1", notification);
            byte[] array = ResourceContainer.Database.ExecuteScalar("events", sql) as byte[];
            string value = array.AsString();

            Single s;
            if (Single.TryParse(value, out s))
            {
                if (s >= thresholdAmount)
                {
                    ruleResult.Score = 1;
                }   
            }

            return ruleResult;
        }
    }
}