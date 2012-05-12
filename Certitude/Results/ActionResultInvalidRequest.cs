using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public class ActionResultInvalidRequest : ActionResult
    {
        public ActionResultInvalidRequest(IModel model, IView view)
            : base(model, view)
        {
        }

    }
}