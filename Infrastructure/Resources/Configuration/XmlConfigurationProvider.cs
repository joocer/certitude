using System;
using System.Collections.Generic;
using System.Xml;

namespace Infrastructure.Resources.Configuration
{
    public class XmlConfigurationProvider : IConfigurationService
    {
        private const string ValueXPath = "/configuration/item[@key=\"{0}\"]/@value";
        private const string EncryptedXPath = "/configuration/item[@key='{0}']/@encrypted";

        // Reading the config is expensive, cache reads 
        private static readonly IDictionary<int, string> ConfigCache = new Dictionary<int, string>();

        public string ReadValue(string section, string key)
        {
            #region check the cache
            // try the cache
            int cacheKey = CacheKey(section, key);
            if (ConfigCache.ContainsKey(cacheKey))
            {
                return ConfigCache[cacheKey];
            }
            #endregion

            #region read xml cache file
            // read the file from config
            XmlDocument document = new XmlDocument();
            document.Load(@"config\" + section + ".xml");

            // this is the payload data
            XmlNode valueNode = document.SelectSingleNode(String.Format(ValueXPath, key));
            if (valueNode == null)
            {
                return String.Empty;
            }
            string value = valueNode.InnerText;
            #endregion

            #region encryption
            // this node is optional, if its not there assume the data's not encrypted
            XmlNode encryptedNode = document.SelectSingleNode(String.Format(EncryptedXPath, key));
            if (encryptedNode != null)
            {
                bool encrypted = bool.TryParse(encryptedNode.InnerText, out encrypted) && encrypted;
                // if the data is encrypted, decrypt it
                if (encrypted)
                {
                    value = AESCryptoProvider.Decrypt(value);
                }
            }
            #endregion

            // add the value to the cache
            ConfigCache.Add(cacheKey, value);

            return value;
        }

        private static int CacheKey(string config, string key)
        {
            string combined = config + ":" + key;
            return combined.GetHashCode();
        }
    }
}
