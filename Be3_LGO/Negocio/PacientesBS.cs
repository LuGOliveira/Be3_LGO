using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Be3_LGO.lib.Persistencia.dbDB;
using Entity = Be3_LGO.lib.Persistencia.dbDB.Entidade;
using Enum = Be3_LGO.lib.Persistencia.dbDB.Enum;

namespace Be3_LGO.lib.Negocio
{
    public class PacienteBS : NegocioAbstract
    {
        public PacienteBS() : base() { }

        public PacienteBS(Contexto contexto) : base(contexto) { }

        public async Task Incluir(Entity.Paciente paciente, List<Inconsistencia> inconsistenciaColecao)
        {
            paciente.AcaoPersistencia = Enum.AcaoPersistencia.Inclusao;

            if (await PacienteValidation.Incluir(paciente, inconsistenciaColecao, Contexto))
            {
                await Incluir(paciente);
            }
        }

        internal async Task Incluir(Entity.Paciente paciente)
        {
            var pacienteDB = new PacienteDB(Contexto);
            await pacienteDB.Incluir(paciente);
        }
    }
}
