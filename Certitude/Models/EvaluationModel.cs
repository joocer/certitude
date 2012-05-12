using Certitude.Models;
using Certitude.Services.Validation.Attributes;

namespace Certitude.Models
{
    public struct EvaluationModel : IModel
    {
        [Required(true)]
        [StringLength(32)]
        public string ClientID { get; set; }

        [Required(true)]
        [StringLength(64)]
        public string AuthenticationToken { get; set; }

        [Required(true)]
        [StringLength(32)]
        public string NotificationID { get; set; }
    }
}