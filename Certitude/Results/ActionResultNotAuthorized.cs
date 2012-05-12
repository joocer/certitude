using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public class ActionResultNotAuthorized : ActionResult
    {
        public ActionResultNotAuthorized(IModel model, IView view) : base(model, view)
        {
        }
    }
}