
namespace Infrastructure.Resources.Hashing
{
    public interface IHashingService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
        bool CheckPassword(string hash, string password, string salt);
    }
}
