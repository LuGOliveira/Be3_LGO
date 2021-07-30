using System.Data.SqlClient;

namespace Be3_LGO.lib.Persistencia.dbDB
{
    public class Contexto
    {
        private SqlConnection _connection;
        private SqlTransaction _transacao;

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public SqlTransaction Transacao
        {
            get
            {
                return _transacao;
            }
            set
            {
                _transacao = value;
            }
        }
    }
}