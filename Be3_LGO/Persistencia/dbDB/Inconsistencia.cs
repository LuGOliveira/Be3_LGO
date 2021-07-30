using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Persistencia.dbDB
{
    public class Inconsistencia
    {
        public string Descricao { get; set; }

        public Guid IdObjeto { get; set; }

        public List<string> CampoColecao { get; set; }

        public Inconsistencia(string descricao)
        {
            Descricao = descricao;
            IdObjeto = Guid.NewGuid();
            CampoColecao = new List<string>();
        }

        public Inconsistencia(string descricao, Guid idObjeto)
        {
            Descricao = descricao;
            IdObjeto = idObjeto;
            CampoColecao = new List<string>();
        }

        public Inconsistencia(string descricao, Guid idObjeto, params string[] campoColecao)
        {
            Descricao = descricao;
            IdObjeto = idObjeto;
            CampoColecao = campoColecao.ToList();
        }
    }

    public static class InconsistenciaExtensions

    {
        public static void Add(this List<Inconsistencia> inconsistenciaColecao, string descricao)
        {
            inconsistenciaColecao.Add(new Inconsistencia(descricao));
        }

        public static void Add(this List<Inconsistencia> inconsistenciaColecao, string descricao, Guid idObjeto)
        {
            inconsistenciaColecao.Add(new Inconsistencia(descricao, idObjeto));
        }

        public static void Add(this List<Inconsistencia> inconsistenciaColecao, string descricao, Guid idObjeto, params string[] campoColecao)
        {
            inconsistenciaColecao.Add(new Inconsistencia(descricao, idObjeto, campoColecao));
        }
    }
}