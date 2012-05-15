using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
        [StringLength(32)]
        public string ClientID { get; set; }

        [Required(true)]
        [StringLength(64)]
        public string AuthenticationToken { get; set; }

        [Required(true)]
        [StringLength(1, 32)]
        public string DetectedBy { get; set; }

        [Required(true)]
        [StringLength(1,32)]
        public string EventType { get; set; }

        [Required(false)]
        [StringLength(0, 32)]
        public IEnumerable<string> SubjectIdentifiers { get; set; }

        [Required(false)]
        [StringLength(0,32)]
        public string DataValue { get; set; }

        [Required(false)]
        [WhiteList("num", "str", "geo")]
        public string DataType { get; set; }

        #endregion
    }
}