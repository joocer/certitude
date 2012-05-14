using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Infrastructure.Resources.Configuration
{
    public class RegistryConfigurationProvider : IConfigurationService
    {
        private const string ApplicationName = "common";
        private const string Author = "joocer";
        private const string FullKey = @"SOFTWARE\{0}\{1}\{2}";
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

            string value = string.Empty;

            string keyName = String.Format(FullKey, Author, ApplicationName, section);
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName);
            if (registryKey != null)
            {
                value = registryKey.GetValue(key) as string;
            }

            #region encryption
            if (!String.IsNullOrEmpty(value))
            {
                bool encrypted = value.StartsWith("encrypted:");
                // if the data is encrypted, decrypt it
                if (encrypted)
                {
                    value = value.Substring("encrypted:".Length);
                    value = AESCryptoProvider.Decrypt(value);
                }
            }
            #endregion

            #region cache and return
            // add the value to the cache
            ConfigCache.Add(cacheKey, value);
            return value;
            #endregion
        }

        private static int CacheKey(string section, string key)
        {
            string combined = section + ":" + key;
            return combined.GetHashCode();
        }
    }
}