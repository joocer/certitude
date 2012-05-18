
namespace Certitude.Models
{
    public interface IModel
    {
        string AuthenticationToken { get; set;  }

        string ClientID { get; set; }
    }
}
