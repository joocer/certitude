namespace Infrastructure.Resources.Configuration
{
    public interface IConfigurationService
    {
        string ReadValue(string section, string key);
    }
}
