using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;


namespace Be3_LGO.lib.Persistencia.dbDB
{
    internal class PreencheEntidade<T> where T : class, new()
    {
        public T GetEntidade(IDataReader dataReader)
        {
            T Entidade = default(T);

            if (dataReader.Read())
            {
                Entidade = new T();
                CarregaEntidade(dataReader, Entidade);
            }
            return Entidade;
        }

        public List<T> GetListaEntidade(IDataReader dataReader, Action<IDataReader, T> createNew = null)
        {
            var EntidadeColecao = new List<T>();

            while (dataReader.Read())
            {
                T Entidade = new T();
                CarregaEntidade(dataReader, Entidade);
                createNew?.Invoke(dataReader, Entidade);
                EntidadeColecao.Add(Entidade);
            }
            return EntidadeColecao;
        }

        private static Dictionary<Type, Dictionary<string, PropertyInfo>> DicionarioResumoClasses = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        private void CarregaEntidade(IDataReader dataReader, T entidade)
        {
            var TipoEntidade = typeof(T);

            if (!DicionarioResumoClasses.ContainsKey(TipoEntidade))
            {
                lock (DicionarioResumoClasses)
                {
                    if (!DicionarioResumoClasses.ContainsKey(TipoEntidade))
                    {
                        var RegistroDePropriedadeColecao = TipoEntidade.GetTypeInfo().GetProperties().AsEnumerable();
                        DicionarioResumoClasses.Add(TipoEntidade, RegistroDePropriedadeColecao.ToDictionary(x => x.Name, x => x));
                    }
                }
            }

            var PropriedadeColecao = DicionarioResumoClasses[TipoEntidade];

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string NomeColuna = dataReader.GetName(i);
                PropertyInfo propriedade = PropriedadeColecao.ContainsKey(NomeColuna) ? PropriedadeColecao[NomeColuna] : null;

                if (propriedade != null)
                {
                    object valor = dataReader.GetValue(i);

                    if (System.DBNull.Equals(valor, DBNull.Value))
                    {
                        valor = null;
                    }
                    else
                    {
                        var tipo = propriedade.PropertyType;

                        if (tipo.GetGenericArguments().Count() > 0)
                        {
                            tipo = tipo.GetGenericArguments().First();
                        }

                        if (tipo.GetTypeInfo().IsEnum)
                        {
                            valor = System.Enum.Parse(tipo, valor.ToString());
                        }
                    }

                    propriedade.SetValue(entidade, valor, null);
                }
            }
        }
    }
}