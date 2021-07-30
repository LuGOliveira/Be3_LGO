using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Persistencia.dbDB.DB
{
    public sealed class dbDBCommand : IDisposable
    {
        private readonly Contexto contexto;
        private readonly DbConnection connection;
        public static string ConnectionString { get; set; }
        public readonly DbCommand Command;

        internal static void NewConnection(Contexto contexto)
        {
            var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            contexto.Connection = Connection;
        }

        public dbDBCommand(Contexto contexto, string commandText, CommandType commandType)
        {
            this.contexto = contexto;
            if (contexto.Connection == null)
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            else
            {
                connection = contexto.Connection;
            }

            Command = connection.CreateCommand();
            Command.CommandText = commandText;
            Command.CommandType = commandType;

            if (contexto.Transacao != null)
            {
                Command.Transaction = contexto.Transacao;
            }
        }
        
        public Task<int> ExecuteNonQueryAsync()
        {
            return Command.ExecuteNonQueryAsync();
        }

        public Task<object> ExecuteScalarAsync()
        {
            return Command.ExecuteScalarAsync();
        }

        public Task<DbDataReader> ExecuteReaderAsync()
        {
            return Command.ExecuteReaderAsync();
        }

        public void Dispose()
        {
            if (contexto.Connection == null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}