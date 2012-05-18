using System.Collections.Generic;
using Infrastructure.Resources.Validation.Attributes;

namespace Certitude.Models
{
    /// <summary>
    /// UI Model of a NotificationModel
    /// </summary>
    public struct NotificationModel : IModel
    {
        #region properties

        [Required(true)]
        [RegularExpression("^[\\da-fA-F]{64}$")] // hex string
        public string AuthenticationToken { get; set; }

        [Required(true)]
        [RegularExpression("^[\\da-fA-F]{32}$")] // hex string
        public string ClientID { get; set; }

        [Required(true)]
        [StringLength(1, 32)]
        public string DetectedBy { get; set; }

        [Required(true)]
        [StringLength(1,16)]
        public string EventType { get; set; }

        [Required(false)]
        [StringLength(0, 32)]
        public IEnumerable<string> SubjectIdentifiers { get; set; }

        [Required(false)]
        [StringLength(0,16)]
        public string DataValue { get; set; }

        [Required(false)]
        [WhiteList("num", "str", "geo")]
        public string DataType { get; set; }

        #endregion
    }
}