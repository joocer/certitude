using System.Collections.Generic;

namespace Infrastructure.Resources.Database
{
    public interface IDatabaseService
    {
        int ExecuteNonQuery(string connection, string command, params string[] parameters);
        object ExecuteScalar(string connection, string command, params string[] parameters);
        IEnumerable<T> ExecuteQuery<T>(string connection, string command, params string[] parameters);
    }
}
