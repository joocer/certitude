using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;

namespace Certitude.Services.Configuration
{
    public class RegistryConfigurationProvider : IConfigurationService
    {
        private readonly string _applicationName;
        private const string FullKey = @"SOFTWARE\{0}\{1}\{2}";
        private readonly string _author;
        private static readonly IDictionary<int, string> ConfigCache = new Dictionary<int, string>();

        public RegistryConfigurationProvider()
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            foreach(Attribute attr in Attribute.GetCustomAttributes(assembly))
            {
                if (attr is AssemblyTitleAttribute)
                {
                    _applicationName = ((AssemblyTitleAttribute) attr).Title;
                }
                if (attr is AssemblyCompanyAttribute)
                {
                    _author = ((AssemblyCompanyAttribute) attr).Company;
                }
            }
        }

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

            string keyName = String.Format(FullKey, _author, _applicationName, section);
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