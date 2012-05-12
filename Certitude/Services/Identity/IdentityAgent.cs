using System;
using System.Security.Cryptography;
using System.Text;

namespace Certitude.Services.Identity
{
    /// <summary>
    /// Default implementation of Authentication Service
    /// </summary>
    public class IdentityAgent : IdentityService
    {
        public IdentityAgent(string clientID)
            : base(clientID)
        { }

        public override bool Authenticate(string authenticationKey)
        {
            // this works by retrieving the client's password from the database we do a SHA256 hash 
            // on clientID + password to see if it matches the authenticationKey

            //TODO: fix this
            return true;

            // get the password from the database
            // TODO: this is IoC, not DI
            string password = ServiceFactory.DatabaseResource.ExecuteNonQuery("authorization", "SELECT secret FROM t_credentials WHERE ClientID = '{0}'", Identity).ToString();

            // hash
            string hash = ComputeHash(Identity, password, new SHA256CryptoServiceProvider());

            // does the hash match the key?
            return authenticationKey == hash;
        }

        /// <summary>
        /// Combine and Hash
        /// </summary>
        /// <param name="clientID">Unique Client Reference</param>
        /// <param name="password">Client Password</param>
        /// <param name="algorithm">Any HashAlgorithm</param>
        /// <returns></returns>
        public static string ComputeHash(string clientID, string password, HashAlgorithm algorithm)
        {
            Byte[] clientIDBytes = Encoding.Unicode.GetBytes(clientID);
            Byte[] passwordBytes = Encoding.Unicode.GetBytes(password);

            // Combine salt and input bytes
            Byte[] combinedInput = new Byte[passwordBytes.Length + clientIDBytes.Length];
            passwordBytes.CopyTo(combinedInput, 0);
            clientIDBytes.CopyTo(combinedInput, passwordBytes.Length);

            // hash and return the result
            Byte[] hashedBytes = algorithm.ComputeHash(combinedInput);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}