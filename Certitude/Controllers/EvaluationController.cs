using System.Collections.Generic;
using Certitude.Results;
using Certitude.Rules;
using Certitude.Views;
using Certitude.Models;

namespace Certitude.Controllers
{
    public class EvaluationController : Controller
    {
        /// <summary>
        /// The intention is for rules to be run against the model
        /// </summary>
        /// <param name="model">NotificationModel to be evaluated</param>
        /// <returns>ActionResult which then provides output back to caller</returns>
        protected override ActionResult DoService(IModel model, string traceID)
        {
            EvaluationModel evaluationModel = (EvaluationModel)model;

            List<RuleResult> results = new List<RuleResult>();

            Dictionary<string,string> config = new Dictionary<string, string>();
            config.Add("event-threshold-value", "800");
            
            IRule rule = new EventThresholdCheck();
            results.Add(rule.Execute(evaluationModel.NotificationID, null));

            // return the result
            return new ActionResultDisplayEvaluation(model, new EvaluationView(results));
        }
    }
}