using System;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Resources;

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
            // fetch the salt from the database
            var saltBytes = ResourceContainer.Database.ExecuteScalar("authorization",
                "CALL sp_GetClientSalt('{0}')",
                Identity) as byte[];
            string salt = saltBytes.AsString();

            // get the password from the database
            var secretBytes = ResourceContainer.Database.ExecuteScalar("authorization",
                "CALL sp_GetClientSecret('{0}')",
                Identity) as byte[];
            string secret = secretBytes.AsString();

            // check the password
            return ResourceContainer.Hashing.CheckPassword(authenticationKey, secret, salt);
        }

        public override void CreateCredentials(string password)
        {
            string salt = ResourceContainer.Hashing.GenerateSalt();
            string hash = ResourceContainer.Hashing.HashPassword(password, salt);

            ResourceContainer.Database.ExecuteNonQuery("authorization",
                "CALL sp_CreateClientSecret('{0}', '{1}', '{2}')",
                Identity, hash, salt);
        }
    }
}