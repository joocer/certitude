using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public class ActionResultFatalError : ActionResult
    {
        public ActionResultFatalError(IModel model, IView view)
            : base(model, view)
        {
        }

    }
}