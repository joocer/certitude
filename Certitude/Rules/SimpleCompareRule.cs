using System;
using Certitude.Services;
using Infrastructure.Resources;
using Infrastructure.Resources.Configuration;

namespace Certitude.Rules
{
    /// <summary>
    /// Simple Rules execute a scalar SQL statement and compares to a fixed input
    /// </summary>
    public class SimpleCompareRule : Rule
    {
        private readonly string _ruleName;
        private readonly IConfigurationService _configuration;

        public SimpleCompareRule(string ruleName, IConfigurationService configuration)
        {
            _ruleName = ruleName;
            _configuration = configuration;
        }

        protected override RuleResult DoService(string notification)
        {
            #region killer questions
            if (!PassesKillerQuestions(notification))
            {
                return null;
            }
            #endregion

            RuleResult ruleResult = new RuleResult(_ruleName);

            Single threshold;
            string thresholdString = GetParameter(_ruleName + "-value");
            if (!Single.TryParse(thresholdString, out threshold))
            {
                throw new RuleExecutionException(_ruleName, String.Format("{0}-value paramater failed to convert to a Single", _ruleName), null);
            }

            // look up the SP based on the rulename
            string sql = _configuration.ReadValue("storedprocedures", _ruleName);
            sql = string.Format(sql, notification);
            byte[] array = ResourceContainer.Database.ExecuteScalar("events", sql) as byte[];
            string value = array.AsString();

            Single s;
            if (Single.TryParse(value, out s))
            {
                if (s >= threshold)
                {
                    ruleResult.Score = 1;
                }
            }

            return ruleResult;
        }

        private bool PassesKillerQuestions(string notification)
        {
            if (String.IsNullOrEmpty(_ruleName))
            {
                throw new RuleExecutionException(String.Empty, "RuleName not set", null);
            }
            if (String.IsNullOrEmpty(notification))
            {
                throw new RuleExecutionException(_ruleName, "Notification not set", null);
            }

            return true;
        }
    }
}