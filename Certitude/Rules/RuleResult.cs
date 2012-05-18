using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Certitude.Rules
{
    public class RuleResult
    {
        private readonly string _name;
        private readonly List<string> _evidence;

        public RuleResult(string name)
        {
            _name = name;
            _evidence = new List<string>();
        }

        public void AddEvidence(string notification)
        {
            _evidence.Add(notification);
        }

        public int Score { get; set; }

        public string ToXml()
        {
            // set up the writer
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("rule");

            xmlWriter.WriteAttributeString("name", _name);
            xmlWriter.WriteAttributeString("score", Score.ToString());

            if (_evidence != null && _evidence.Count > 0)
            {
                foreach (string notification in _evidence)
                {
                    xmlWriter.WriteStartElement("evidence");
                    xmlWriter.WriteAttributeString("notification", notification);
                    xmlWriter.WriteEndElement();   
                }
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return stringBuilder.ToString();
        }
    }
}