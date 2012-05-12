using System.Collections.Generic;

namespace Certitude.Services.Database
{
    public interface IDatabaseResource
    {
        int ExecuteNonQuery(string connection, string command, params string[] parameters);
        T ExecuteScalar<T>(string connection, string command, params string[] parameters);
        object ExecuteScalar(string connection, string command, params string[] parameters);
        IEnumerable<T> ExecuteQuery<T>(string connection, string command, params string[] parameters);
    }
}
