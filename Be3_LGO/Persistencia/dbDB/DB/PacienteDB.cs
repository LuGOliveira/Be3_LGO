using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Be3_LGO.lib.Persistencia.dbDB
{
    internal partial class PacienteDB : DB.DBAbstract
    {
        internal PacienteDB(Contexto contexto) : base(contexto)
        { }

        internal async Task Incluir(Entidade.Paciente paciente)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("Exec dbo.Sp_be3_Ins_Paciente  ");
            SQL.AppendLine("     @Prontuario, ");
            SQL.AppendLine("     @Nome, ");
            SQL.AppendLine("     @Sobrenome, ");
            SQL.AppendLine("     @DataNascimento, ");
            SQL.AppendLine("     @@Sexo, ");
            SQL.AppendLine("     @Genero, ");
            SQL.AppendLine("     @CPF, ");
            SQL.AppendLine("     @RG, ");
            SQL.AppendLine("     @UfRG, ");
            SQL.AppendLine("     @Email, ");
            SQL.AppendLine("     @Celular, ");
            SQL.AppendLine("     @Telefone, ");
            SQL.AppendLine("     @PlanoConvenio, ");
            SQL.AppendLine("     @Carteirinha, ");
            SQL.AppendLine("     @ValidadeCarteirinha, ");
            SQL.AppendLine("     @IdPaciente ");

            using (var Command = NewCommand(SQL.ToString()))
            {
                ParametroEntrada(Command, "Prontuario", paciente.Prontuario);
                ParametroEntrada(Command, "Nome", paciente.Nome);
                ParametroEntrada(Command, "Sobrenome", paciente.Sobrenome);
                ParametroEntrada(Command, "DataNascimento", paciente.DataNascimento);
                ParametroEntrada(Command, "Sexo", paciente.Sexo);
                ParametroEntrada(Command, "Genero", paciente.Genero);
                ParametroEntrada(Command, "CPF", paciente.CPF);
                ParametroEntrada(Command, "RG", paciente.RG);
                ParametroEntrada(Command, "UfRG", paciente.UfRG);
                ParametroEntrada(Command, "Email", paciente.Email);
                ParametroEntrada(Command, "Celular", paciente.Celular);
                ParametroEntrada(Command, "Telefone", paciente.Telefone);
                ParametroEntrada(Command, "PlanoConvenio", paciente.PlanoConvenio);
                ParametroEntrada(Command, "Carteirinha", paciente.Carteirinha);
                ParametroEntrada(Command, "ValidadeCarteirinha", paciente.ValidadeCarteirinha);

                var Identity = ParametroSaida(Command, "IdPaciente", DbType.Int32);

                await Command.ExecuteNonQueryAsync();

                //Retorna valor do Identity
                paciente.IdPaciente = (int)Identity.Value;
            }
        }

        internal async Task Alterar(Entidade.Paciente paciente)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("Exec dbo.Sp_be3_Upd_Paciente ");
            SQL.AppendLine("     @IdPaciente, ");
            SQL.AppendLine("     @Prontuario, ");
            SQL.AppendLine("     @Nome, ");
            SQL.AppendLine("     @Sobrenome, ");
            SQL.AppendLine("     @DataNascimento, ");
            SQL.AppendLine("     @@Sexo, ");
            SQL.AppendLine("     @Genero, ");
            SQL.AppendLine("     @CPF, ");
            SQL.AppendLine("     @RG, ");
            SQL.AppendLine("     @UfRG, ");
            SQL.AppendLine("     @Email, ");
            SQL.AppendLine("     @Celular, ");
            SQL.AppendLine("     @Telefone, ");
            SQL.AppendLine("     @PlanoConvenio, ");
            SQL.AppendLine("     @Carteirinha, ");
            SQL.AppendLine("     @ValidadeCarteirinha ");
            
            using (var Command = NewCommand(SQL.ToString()))
            {
                ParametroEntrada(Command, "IdPaciente", paciente.IdPaciente);
                ParametroEntrada(Command, "Prontuario", paciente.Prontuario);
                ParametroEntrada(Command, "Nome", paciente.Nome);
                ParametroEntrada(Command, "Sobrenome", paciente.Sobrenome);
                ParametroEntrada(Command, "DataNascimento", paciente.DataNascimento);
                ParametroEntrada(Command, "Sexo", paciente.Sexo);
                ParametroEntrada(Command, "Genero", paciente.Genero);
                ParametroEntrada(Command, "CPF", paciente.CPF);
                ParametroEntrada(Command, "RG", paciente.RG);
                ParametroEntrada(Command, "UfRG", paciente.UfRG);
                ParametroEntrada(Command, "Email", paciente.Email);
                ParametroEntrada(Command, "Celular", paciente.Celular);
                ParametroEntrada(Command, "Telefone", paciente.Telefone);
                ParametroEntrada(Command, "PlanoConvenio", paciente.PlanoConvenio);
                ParametroEntrada(Command, "Carteirinha", paciente.Carteirinha);
                ParametroEntrada(Command, "ValidadeCarteirinha", paciente.ValidadeCarteirinha);

                await Command.ExecuteNonQueryAsync();

            }
        }

        internal async Task<Entidade.Paciente> Obter(int idPaciente)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("Exec dbo.Sp_be3_Con_Paciente ");
            SQL.AppendLine("     @IdPaciente ");

            using (var Command = NewCommand(SQL.ToString()))
            {
                ParametroEntrada(Command, "IdPaciente", idPaciente);

                using (var DataReader = await Command.ExecuteReaderAsync())
                {
                    return new PreencheEntidade<Entidade.Paciente>().GetEntidade(DataReader);
                }
            }
        }

        internal async Task<int> ExistePor_CPF(string cpf)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine(" select count(0)");
            SQL.AppendLine("   from Paciente with (nolock)");
            SQL.AppendLine("  where CPF = @CPF");

            using (var Command = NewCommand(SQL.ToString()))
            {
                ParametroEntrada(Command, "CPF", cpf);

                var totalRegistros = await Command.ExecuteScalarAsync();

                return (int)totalRegistros;
            }
        }
    }
}