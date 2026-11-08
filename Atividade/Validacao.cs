using System;
using System.Globalization;
using System.Linq;

namespace Atividade
{
    // Leitura de dados pelo console com tratamento dos valores não permitidos.
    // Cada método repete a pergunta até o usuário digitar algo válido.
    static class Validacao
    {
        public static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.WriteLine(mensagem);
                string lido = Console.ReadLine();
                if (lido == null)            // fim da entrada (Ctrl+Z / Ctrl+D): encerra
                    Environment.Exit(0);
                string entrada = lido.Trim();
                if (entrada != "")
                    return entrada;
                Console.WriteLine("Campo obrigatório. Tente novamente.");
            }
        }

        public static string LerTipo()
        {
            while (true)
            {
                string tipo = LerTexto("Pessoa Física (f) ou Jurídica (j) ?").ToLower();
                if (tipo == "f" || tipo == "j")
                    return tipo;
                Console.WriteLine("Opção inválida. Digite f ou j.");
            }
        }

        // Aceita vírgula ou ponto como separador decimal (ex.: 1500,50 ou 1500.50).
        public static float LerValor(string mensagem)
        {
            while (true)
            {
                string entrada = LerTexto(mensagem).Replace(",", ".");
                if (float.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out float valor) && valor > 0)
                    return valor;
                Console.WriteLine("Valor inválido. Digite um número maior que zero.");
            }
        }

        public static string LerCpf()
        {
            while (true)
            {
                string cpf = SomenteDigitos(LerTexto("Informar CPF:"));
                if (CpfValido(cpf))
                    return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
                Console.WriteLine("CPF inválido. Tente novamente.");
            }
        }

        public static string LerCnpj()
        {
            while (true)
            {
                string cnpj = SomenteDigitos(LerTexto("Informar CNPJ:"));
                if (CnpjValido(cnpj))
                    return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
                Console.WriteLine("CNPJ inválido. Tente novamente.");
            }
        }

        static string SomenteDigitos(string texto) => new string(texto.Where(char.IsDigit).ToArray());

        // Valida os dois dígitos verificadores do CPF.
        public static bool CpfValido(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;
            int[] d = cpf.Select(c => c - '0').ToArray();
            for (int pos = 9; pos <= 10; pos++)
            {
                int soma = 0;
                for (int i = 0; i < pos; i++)
                    soma += d[i] * (pos + 1 - i);
                int dv = soma * 10 % 11 % 10;
                if (dv != d[pos])
                    return false;
            }
            return true;
        }

        // Valida os dois dígitos verificadores do CNPJ.
        public static bool CnpjValido(string cnpj)
        {
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
                return false;
            int[] d = cnpj.Select(c => c - '0').ToArray();
            int[] pesos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int Dv(int[] pesos)
            {
                int soma = 0;
                for (int i = 0; i < pesos.Length; i++)
                    soma += d[i] * pesos[i];
                int resto = soma % 11;
                return resto < 2 ? 0 : 11 - resto;
            }
            return Dv(pesos1) == d[12] && Dv(pesos2) == d[13];
        }
    }
}
