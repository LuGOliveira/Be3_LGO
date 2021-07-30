using Be3_LGO.lib.Persistencia.dbDB;
using Be3_LGO.lib.Persistencia.dbDB.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Persistencia.dbDB.Entidade
{
    public class Paciente : EntityAbstract
    {
        //Propriedades
        //============
        public int IdPaciente { get; set; }
        public string Descricao { get; set; }
        public string DescricaoDetalhada { get; set; }
        public string Prontuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Genero { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string UfRG { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public string PlanoConvenio { get; set; }
        public string Carteirinha { get; set; }
        public DateTime ValidadeCarteirinha { get; set; }

        //Navegação
        //=========
        public Paciente()
        {
        }

        //Validações de Campos
        //====================
        public override List<Inconsistencia> Validar()
        {
            var InconsistenciaColecao = new List<Inconsistencia>();

            //Validações de Obrigatoriedade
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                InconsistenciaColecao.Add("O campo 'Nome' precisa ser informado.", this.IdObjeto, "Nome");
            }
            if (string.IsNullOrWhiteSpace(this.Nome))
            {
                InconsistenciaColecao.Add("O campo 'Sobrenome' precisa ser informado.", this.IdObjeto, "Sobrenome");
            }
            if (string.IsNullOrWhiteSpace(this.Email))
            {
                InconsistenciaColecao.Add("O campo 'Email' precisa ser informado.", this.IdObjeto, "Email");
            }
            if ((string.IsNullOrWhiteSpace(this.Celular)) && (string.IsNullOrWhiteSpace(this.Telefone)))
            {
                InconsistenciaColecao.Add("Um dos campos 'Celular' ou 'Telefone', precisa ser informado.", this.IdObjeto, "Celular");
            }

            //Validações de Tamanho
            if (this.Prontuario != null && this.Prontuario.Length > 1000)
            {
                InconsistenciaColecao.Add("O campo 'Prontuario' não pode conter mais que 1000 caracteres.", this.IdObjeto, "Prontuario");
            }

            if (this.Nome != null && this.Nome.Length > 20)
            {
                InconsistenciaColecao.Add("O campo 'Nome' não pode conter mais que 20 caracteres.", this.IdObjeto, "Nome");
            }

            if (this.Sobrenome != null && this.Sobrenome.Length > 100)
            {
                InconsistenciaColecao.Add("O campo 'Sobrenome' não pode conter mais que 100 caracteres.", this.IdObjeto, "Sobrenome");
            }
            if (this.Email != null && this.Email.Length > 100)
            {
                InconsistenciaColecao.Add("O campo 'Email' não pode conter mais que 100 caracteres.", this.IdObjeto, "Email");
            }
            if (this.PlanoConvenio != null && this.PlanoConvenio.Length > 30)
            {
                InconsistenciaColecao.Add("O campo 'Plano do Convênio' não pode conter mais que 30 caracteres.", this.IdObjeto, "PlanoConvenio");
            }
            if (this.Carteirinha != null && this.Carteirinha.Length > 30)
            {
                InconsistenciaColecao.Add("O campo 'Carteirinha' não pode conter mais que 16 caracteres.", this.IdObjeto, "Carteirinha");
            }

            return InconsistenciaColecao;
        }
    }
}
