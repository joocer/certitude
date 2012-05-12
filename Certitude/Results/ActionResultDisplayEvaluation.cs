using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public class ActionResultDisplayEvaluation : ActionResult
    {
        public ActionResultDisplayEvaluation(IModel model, IView view) 
            : base(model, view)
        {
        }
    }
}