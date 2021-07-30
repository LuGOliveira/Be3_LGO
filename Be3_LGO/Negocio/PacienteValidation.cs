using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = Be3_LGO.lib.Persistencia.dbDB.Entidade;
using Enum = Be3_LGO.lib.Persistencia.dbDB.Enum;
using System.Configuration;
using Be3_LGO.lib.Persistencia.dbDB;
using System.Text;
using Be3_LGO.lib.Uteis;

namespace Be3_LGO.lib.Negocio
{
    public static class PacienteValidation
    {
        internal static async Task<bool> Incluir(Entity.Paciente paciente, List<Inconsistencia> inconsistenciaColecao, Contexto contexto)
        {
            var totalMsgInicio = inconsistenciaColecao.Count();

            //Inicia a DB
            //===========
            var pacienteDB = new PacienteDB(contexto);

            //Valida proprieddades da Entidade
            //================================
            inconsistenciaColecao.AddRange(paciente.Validar());

            //Verifica se o CPF é válido
            //==========================
            if (!Validacoes.ValidaCPF(paciente.CPF))
            {
                inconsistenciaColecao.Add(new Inconsistencia("O CPF do paciênte é inválido"));
            }

            //Verifica se o Email é válido
            //============================
            if (!Validacoes.EmailValido(paciente.Email))
            {
                inconsistenciaColecao.Add(new Inconsistencia("O E-MAIL do paciente é inválido"));
            }

            //Verifica se o Paciente já existe na base através do CPF
            //========================================================
            if (await pacienteDB.ExistePor_CPF(paciente.CPF) > 0)
            {
                inconsistenciaColecao.Add(new Inconsistencia("CPF informado já está associado a outro Paciente"));
            }

            //Retorno
            //=======
            return totalMsgInicio == inconsistenciaColecao.Count();
        }
    }
}