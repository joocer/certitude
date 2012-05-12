using Certitude.Models;

namespace Certitude.Views
{
    public interface IView
    {
        string Serialize(IModel model);
    }
}
