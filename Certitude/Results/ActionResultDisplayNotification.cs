using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public class ActionResultDisplayNotification : ActionResult
    {
        public ActionResultDisplayNotification(IModel model, IView view) 
            : base(model, view)
        {
        }
    }
}