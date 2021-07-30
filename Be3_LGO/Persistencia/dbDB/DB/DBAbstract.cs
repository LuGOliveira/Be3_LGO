using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Be3_LGO.lib.Persistencia.dbDB.DB
{
    public abstract class DBAbstract
    {
        protected internal Contexto Contexto { get; private set; }

        internal DBAbstract(Contexto contexto)
        {
            Contexto = contexto;
        }

        protected internal dbDBCommand NewCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var Command = new dbDBCommand(Contexto, commandText, commandType);
            return Command;
        }

        protected void ParametroEntrada(dbDBCommand contextCommand, string nomeParametro, object valorParametro)
        {
            ParametroEntrada(contextCommand.Command, nomeParametro, valorParametro);
        }

        protected void ParametroEntrada(DbCommand comando, string nomeParametro, object valorParametro)
        {
            object ValorBanco = null;
            if (valorParametro != null)
            {
                if (valorParametro.GetType().GetTypeInfo().IsEnum)
                {
                    if (Convert.ToInt16(valorParametro) == 0)
                    {
                        ValorBanco = DBNull.Value;
                    }
                    else
                    {
                        ValorBanco = Convert.ToInt16(valorParametro);
                    }
                }
                else
                {
                    if (valorParametro is DateTime)
                    {
                        ValorBanco = ((DateTime)valorParametro).ToLocalTime();
                    }
                    else
                    {
                        ValorBanco = valorParametro;
                    }
                }
            }
            else
            {
                ValorBanco = DBNull.Value;
            }

            SqlParameter Parametro = new SqlParameter(nomeParametro, this.RecuperaDBType(ValorBanco));
            Parametro.Direction = ParameterDirection.Input;
            Parametro.Value = ValorBanco;

            comando.Parameters.Add(Parametro);
        }

        protected SqlParameter ParametroSaida(dbDBCommand contextCommand, string nomeParametro, DbType tipoParametro)
        {
            return ParametroSaida(contextCommand.Command, nomeParametro, tipoParametro);
        }

        protected SqlParameter ParametroSaida(DbCommand comando, string nomeParametro, DbType tipoParametro)
        {
            SqlParameter Parametro = new SqlParameter(nomeParametro, tipoParametro);
            Parametro.Direction = ParameterDirection.Output;
            comando.Parameters.Add(Parametro);

            return Parametro;
        }

        private DbType RecuperaDBType(object valor)
        {
            if ((valor == null))
            {
                return DbType.String;
            }
            if (valor.GetType().Name == "String")
            {
                return DbType.String;
            }
            if (valor.GetType().Name == "Decimal")
            {
                return DbType.Decimal;
            }
            if (valor.GetType().Name == "Boolean")
            {
                return DbType.Boolean;
            }
            if (valor.GetType().Name == "Date")
            {
                return DbType.Date;
            }
            if (valor.GetType().Name == "DateTime")
            {
                return DbType.DateTime;
            }
            if (valor.GetType().Name == "Short" | valor.GetType().Name == "Int16")
            {
                return DbType.Int16;
            }
            if (valor.GetType().Name == "Integer" | valor.GetType().Name == "Int32")
            {
                return DbType.Int32;
            }
            if (valor.GetType().Name == "Long" | valor.GetType().Name == "Int64")
            {
                return DbType.Int64;
            }
            if (valor.GetType().Name == "Single")
            {
                return DbType.Single;
            }
            if (valor.GetType().Name == "Double")
            {
                return DbType.Double;
            }
            if (valor.GetType().Name == "Byte")
            {
                return DbType.Byte;
            }
            if (valor.GetType().Name == "Binary" | valor.GetType().Name == "Byte[]")
            {
                return DbType.Binary;
            }
            if (valor.GetType().Name == "Char")
            {
                return DbType.String;
            }
            if (valor.GetType().Name == "Guid")
            {
                return DbType.Guid;
            }
            return DbType.String;
        }
    }
}