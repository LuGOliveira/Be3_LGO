using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Be3_LGO.lib.Uteis
{
	public static class Validacoes
	{
		public static bool ValidaCPF(string cpf)
		{
			if (string.IsNullOrEmpty(cpf) || string.IsNullOrWhiteSpace(cpf))
				return false;

			int[] multiplicador = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			
			string tempCpf;
			string digito;

			int soma;
			int resto;

			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");

			if (cpf.Length != 11)
			{
				return false;
			}

			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
			{
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador[i];
			}

			resto = soma % 11;
			if (resto < 10)
			{
				digito = "0" + resto.ToString();
			}
			else
			{
				digito = resto.ToString();
			}

			tempCpf += digito;
			return cpf.EndsWith(digito);
		}

		public static bool EmailValido(string email)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
				return false;

			string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
			if (Regex.IsMatch(email, strModelo))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}