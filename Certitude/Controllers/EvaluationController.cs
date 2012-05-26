using System.Collections.Generic;
using Certitude.Results;
using Certitude.Rules;
using Certitude.Views;
using Certitude.Models;
using Infrastructure.Resources.Configuration;

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

            XmlConfigurationProvider configuration = new XmlConfigurationProvider(@"D:\Code\Certitude\");

            Dictionary<string,string> parameters = new Dictionary<string, string>();
            parameters.Add("compare-event-value-value", "800");

            IRule rule = new SimpleCompareRule("compare-event-value", configuration);
            results.Add(rule.Execute(evaluationModel.NotificationID, parameters));

            // return the result
            return new ActionResultDisplayEvaluation(model, new EvaluationView(results));
        }
    }
}