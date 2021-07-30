using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Uteis
{
    public class Conexao : IDisposable
    {
        public static string ConnectionString { get; set; }

        private DbConnection Connection { get; set; }

        public Conexao()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public Task<int> ExecuteNonQueryAsync(string commandText, CommandType commandType)
        {
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            return command.ExecuteNonQueryAsync();
        }

        public Task<object> ExecuteScalarAsync(string commandText, CommandType commandType)
        {
            var command = Connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            return command.ExecuteScalarAsync();
        }

        public Task<DbDataReader> ExecuteReaderAsync(string commandText, CommandType commandType)
        {
            var Command = Connection.CreateCommand();
            Command.CommandText = commandText;
            Command.CommandType = commandType;
            return Command.ExecuteReaderAsync();
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
