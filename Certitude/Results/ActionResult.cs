using System.Xml;
using Certitude.Views;
using Certitude.Models;

namespace Certitude.Results
{
    public abstract class ActionResult
    {
        private readonly IModel _model;
        private readonly IView _view;

        protected ActionResult(IModel model, IView view)
        {
            _model = model;
            _view = view;
        }

        public string Render()
        {
            return _view.Serialize(_model);
        }
    }
}