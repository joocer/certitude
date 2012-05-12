namespace Certitude.Services.Configuration
{
    public interface IConfigurationService
    {
        string ReadValue(string section, string key);
    }
}
