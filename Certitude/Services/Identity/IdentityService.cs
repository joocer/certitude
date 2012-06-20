namespace Certitude.Services.Identity
{
    public abstract class IdentityService
    {
        private readonly string _clientID;

        protected IdentityService(string clientID)
        {
            _clientID = clientID;
        }

        public string Identity { get { return _clientID; } }

        public abstract bool Authenticate(string authenticationKey);

        public abstract void CreateCredentials(string password);
    }
}