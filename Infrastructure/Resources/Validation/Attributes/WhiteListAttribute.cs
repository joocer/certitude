using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Resources.Validation.Attributes
{
    public class WhiteListAttribute : ValidationAttribute
    {
        private readonly IEnumerable<string> _whitelist;

        public WhiteListAttribute(params string[] whitelist)
            : base(String.Format("Validation failed for field: {0} - field must be one of these values: {1}", "{0}", ToCsv(whitelist.ToList())))
        {
            _whitelist = whitelist;
        }

        public override bool Test(object subject)
        {
            if (subject is String)
            {
                string testsubject = (string)subject;

                if (String.IsNullOrEmpty(testsubject))
                {
                    return true;
                }

                return _whitelist.Contains(testsubject);
            }

            // not sure what the value is
            return true;
        }

        private static string ToCsv(List<string> items)
        {
            var csvBuilder = new StringBuilder();
            for (int i = 0; i < items.Count() - 1; i++)
            {
                csvBuilder.Append("'" + items[i] + "', ");
            }
            csvBuilder.Append("'" + items.LastOrDefault() + "'");
            return csvBuilder.ToString();
        }
    }
}
