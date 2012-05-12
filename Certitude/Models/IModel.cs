
namespace Certitude.Models
{
    public interface IModel
    {
        string ClientID { get; set; }

        string AuthenticationToken { get; set; }
    }
}
