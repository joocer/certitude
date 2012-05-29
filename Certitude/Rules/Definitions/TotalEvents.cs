using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Certitude.Rules.Aggregators;

namespace Certitude.Rules.Definitions
{
    public class TotalEvents : Rule
    {
        protected override RuleResult DoService(string notification)
        {
            RuleResult res = new RuleResult(GetType().Name);

            Count counter = new Count();
            counter.Command = "sp_countevents";

            res.AddEvidence(counter.Calculate().ToString());

            return res;
        }
    }
}