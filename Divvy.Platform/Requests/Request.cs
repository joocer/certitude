namespace Divvy.Platform.Requests
{
    struct Request
    {
        public string Version;
        public string Source;
        public string[] Libraries;
        public bool Compressed;
    }
}
