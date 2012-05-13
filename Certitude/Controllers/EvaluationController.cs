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
            List<string> results = new List<string>();
            
            IRule rule = new DataValueCheck(800);
            string result = rule.Execute(traceID);
            if (!string.IsNullOrEmpty(result))
            {
                results.Add(result);
            }

            // return the result
            return new ActionResultDisplayEvaluation(model, new EvaluationView(results));
        }
    }
}