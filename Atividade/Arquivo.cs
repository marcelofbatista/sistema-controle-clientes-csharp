using System;
using System.Globalization;
using System.IO;

namespace Atividade
{
    // Armazena os registros em arquivo (clientes.csv, separado por ponto e vírgula).
    static class Arquivo
    {
        const string Caminho = "clientes.csv";
        const string Cabecalho = "tipo;nome;endereco;documento;documento2;valor;imposto;total;data_cadastro";
        static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        public static void Salvar(Clientes c)
        {
            string tipo, doc1, doc2;
            if (c is Pessoa_Fisica pf) { tipo = "PF"; doc1 = pf.cpf; doc2 = pf.rg; }
            else if (c is Pessoa_Juridica pj) { tipo = "PJ"; doc1 = pj.cnpj; doc2 = pj.ie; }
            else throw new InvalidOperationException("Tipo de cliente desconhecido.");

            if (!File.Exists(Caminho))
                File.WriteAllText(Caminho, Cabecalho + Environment.NewLine);

            string linha = string.Join(";",
                tipo, Limpar(c.nome), Limpar(c.endereco), Limpar(doc1), Limpar(doc2),
                c.valor.ToString("F2", Inv), c.valor_imposto.ToString("F2", Inv), c.total.ToString("F2", Inv),
                DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            File.AppendAllText(Caminho, linha + Environment.NewLine);
        }

        public static void Listar()
        {
            if (!File.Exists(Caminho))
            {
                Console.WriteLine("Nenhum cliente cadastrado ainda.");
                return;
            }
            string[] linhas = File.ReadAllLines(Caminho);
            Console.WriteLine($"-------- Clientes cadastrados ({linhas.Length - 1}) --------");
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] p = linhas[i].Split(';');
                if (p.Length < 9) continue;
                float total = float.Parse(p[7], Inv);
                Console.WriteLine($"{i}. [{p[0]}] {p[1]} | {p[3]} | Total: {total:C} | {p[8]}");
            }
        }

        // Evita quebrar o arquivo se o usuário digitar ";".
        static string Limpar(string texto) => (texto ?? "").Replace(";", ",");
    }
}
