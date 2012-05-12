using System;
using MySql.Data.MySqlClient;

namespace Certitude.Services.Database
{
    public class MySqlDatabaseResourceAgent : IDatabaseResource
    {
        private static MySqlConnection GetConnection(string connectionName)
        {
            // look up the connection string from a config file
            // TODO: this is IoC, not DI
            string connStr = ServiceFactory.ConfigurationService.ReadValue("connectionstrings", connectionName);

            // establish the connection
            MySqlConnection connection = new MySqlConnection(connStr);
            connection.Open();

            // return back to caller
            return connection;
        }

        public int ExecuteNonQuery(string connection, string command, params string[] parameters)
        {
            string sql = String.Format(command, parameters);
            MySqlConnection mySqlConnection = GetConnection(connection);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);

            return mySqlCommand.ExecuteNonQuery();
        }


        public T ExecuteScalar<T>(string connection, string command, params string[] parameters)
        {
            return (T)ExecuteScalar(connection, command, parameters);
        }


        public object ExecuteScalar(string connection, string command, params string[] parameters)
        {
            string sql = String.Format(command, parameters);
            MySqlConnection mySqlConnection = GetConnection(connection);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);

            return mySqlCommand.ExecuteScalar();
        }

        public System.Collections.Generic.IEnumerable<T> ExecuteQuery<T>(string connection, string command, params string[] parameters)
        {
            MySqlConnection mySqlConnection = GetConnection(connection);

            throw new System.NotImplementedException();
        }
    }
}
