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
            string thresholdEvent = GetParameter("subject-threshold-event");

            string sql = string.Format("SELECT SUM(satellite.datavalue) " +
                                       "FROM  t_events base, " +
                                       "      t_events satellite " +
                                       "WHERE satellite.subjectid = base.subjectid " +
                                       "  AND satellite.eventtype LIKE base.eventtype " +
                                       "  AND satellite.`TimeStamp` > '{0}' " +
                                       "  AND base.traceid = UNHEX('{1}')'",
                                       ResourceContainer.Clock.Now().AddHours(0 - thresholdHours),
                                       notification);
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