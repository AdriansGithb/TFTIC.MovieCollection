using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(string connectionString)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(nameof(connectionString));

            _connectionString = connectionString;

        }

        public int ExecuteNonQuery(Command command)
        {
            using (SqlConnection dbConnection = CreateConnection(_connectionString))
            {
                using (SqlCommand sqlCommand = CreateCommand(dbConnection, command))
                {                   
                    dbConnection.Open();

                    return sqlCommand.ExecuteNonQuery();
                }                
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (SqlConnection dbConnection = CreateConnection(_connectionString))
            {
                using (SqlCommand dbCommand = CreateCommand(dbConnection, command))
                {
                    dbConnection.Open();
                    object result = dbCommand.ExecuteScalar();
                    return result is DBNull ? null : result;
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector, bool executeImmediately)
        {
            return executeImmediately ? ExecuteReader(command, selector).ToList() : ExecuteReader(command, selector);
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            using (SqlConnection dbConnection = CreateConnection(_connectionString))
            {
                using (SqlCommand dbCommand = CreateCommand(dbConnection, command))
                {
                    dbConnection.Open();
                    using(IDataReader reader = dbCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return selector(reader);
                        }
                    }
                }
            }
        }

        public DataTable GetDataTable(Command command)
        {
            using (SqlConnection dbConnection = CreateConnection(_connectionString))
            {
                using (SqlCommand dbCommand = CreateCommand(dbConnection, command))
                {
                    using(SqlDataAdapter dbDataAdapter = new SqlDataAdapter())
                    {
                        DataTable dataTable = new DataTable();
                        dbDataAdapter.SelectCommand = dbCommand;
                        dbDataAdapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
        }

        private static SqlConnection CreateConnection(string connectionString)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = connectionString;

            return dbConnection;
        }

        private static SqlCommand CreateCommand(SqlConnection connection, Command command)
        {
            SqlCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = command.Query;
            if (command.IsStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            foreach (Parameter parameter in command.Parameters.Values)
            {
                SqlParameter sqlParameter = dbCommand.CreateParameter();
                sqlParameter.ParameterName = parameter.Name;
                sqlParameter.Value = parameter.Value;
                dbCommand.Parameters.Add(sqlParameter);
            }

            return dbCommand;
        }
    }
}
