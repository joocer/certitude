using System;
using MySql.Data.MySqlClient;

namespace Infrastructure.Resources.Database
{
    public class MySqlDatabaseResourceAgent : IDatabaseService
    {
        private static MySqlConnection GetConnection(string connectionName)
        {
            // look up the connection string from a config file
            string connStr = ResourceContainer.Configuration.ReadValue("connectionstrings", connectionName);

            // establish the connection
            MySqlConnection connection = new MySqlConnection(connStr);
//            connection.Open();

            // return back to caller
            return connection;
        }

        public int ExecuteNonQuery(string connection, string command, params string[] parameters)
        {
            string sql = String.Format(command, parameters);
            MySqlConnection mySqlConnection = GetConnection(connection);
            MySqlCommand myCommand = new MySqlCommand(sql, mySqlConnection);
            myCommand.Connection.Open();
            int value = myCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return value;
        }

        public object ExecuteScalar(string connection, string command, params string[] parameters)
        {
            string sql = String.Format(command, parameters);
            MySqlConnection mySqlConnection = GetConnection(connection);
            MySqlCommand myCommand = new MySqlCommand(sql, mySqlConnection);
            myCommand.Connection.Open();
            object value = myCommand.ExecuteScalar();
            mySqlConnection.Close();

            return value;
        }

        public System.Collections.Generic.IEnumerable<T> ExecuteQuery<T>(string connection, string command, params string[] parameters)
        {
            MySqlConnection mySqlConnection = GetConnection(connection);

            throw new NotImplementedException();
        }
    }
}
