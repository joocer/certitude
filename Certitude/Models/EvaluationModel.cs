using Infrastructure.Resources.Validation.Attributes;

namespace Certitude.Models
{
    public struct EvaluationModel : IModel
    {
        [Required(true)]
        [RegularExpression("^[\\da-fA-F]{64}$")] // hex string
        public string AuthenticationToken { get; set; }

        [Required(true)]
        [RegularExpression("^[\\da-fA-F]{32}$")] // hex string
        public string ClientID { get; set; }

        [Required(true)]
        [RegularExpression("^[\\da-fA-F]{32}$")] // hex string
        public string NotificationID { get; set; }
    }
}