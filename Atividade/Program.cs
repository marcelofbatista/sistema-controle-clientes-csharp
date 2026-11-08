using System;
using System.Globalization;

namespace Atividade
{
    class Program
    {
        static void Main(string[] args)
        {
            // Moeda em Real (R$) independentemente da configuração do computador.
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("===== ClientLab - Controle de Clientes =====");
                Console.WriteLine("1 - Cadastrar cliente");
                Console.WriteLine("2 - Listar clientes cadastrados");
                Console.WriteLine("0 - Sair");
                string entrada = Validacao.LerTexto("Escolha uma opção:");

                if (!int.TryParse(entrada, out opcao) || opcao < 0 || opcao > 2)
                {
                    Console.WriteLine("Opção inválida.");
                    opcao = -1;
                    continue;
                }

                if (opcao == 1)
                    Cadastrar();
                else if (opcao == 2)
                    Arquivo.Listar();
            }
            Console.WriteLine("Sistema encerrado.");
        }

        static void Cadastrar()
        {
            string var_nome = Validacao.LerTexto("Informar Nome");
            string var_endereco = Validacao.LerTexto("Informar Endereço");
            string var_tipo = Validacao.LerTipo();

            // POLIMORFISMO: a variável é do tipo da classe-pai (Clientes),
            // mas guarda um objeto Pessoa_Fisica ou Pessoa_Juridica.
            Clientes cliente;

            if (var_tipo == "f")
            {
                Pessoa_Fisica pf = new Pessoa_Fisica();
                pf.cpf = Validacao.LerCpf();
                pf.rg = Validacao.LerTexto("Informar RG:");
                cliente = pf;
            }
            else
            {
                Pessoa_Juridica pj = new Pessoa_Juridica();
                pj.cnpj = Validacao.LerCnpj();
                pj.ie = Validacao.LerTexto("Informar IE:");
                cliente = pj;
            }

            cliente.nome = var_nome;
            cliente.endereco = var_endereco;

            float val_pag = Validacao.LerValor("Informar Valor de Compra:");

            // A mesma chamada executa 10% (PF) ou 20% (PJ), conforme o objeto real.
            cliente.Pagar_Imposto(val_pag);

            Exibir(cliente);
            Arquivo.Salvar(cliente);
            Console.WriteLine("Cliente salvo em clientes.csv");
        }

        static void Exibir(Clientes c)
        {
            if (c is Pessoa_Fisica pf)
            {
                Console.WriteLine("-------- Pessoa Física ---------");
                Console.WriteLine("Nome ..........: " + pf.nome);
                Console.WriteLine("Endereço ......: " + pf.endereco);
                Console.WriteLine("CPF ...........: " + pf.cpf);
                Console.WriteLine("RG ............: " + pf.rg);
            }
            else if (c is Pessoa_Juridica pj)
            {
                Console.WriteLine("-------- Pessoa Jurídica ---------");
                Console.WriteLine("Nome ..........: " + pj.nome);
                Console.WriteLine("Endereço ......: " + pj.endereco);
                Console.WriteLine("CNPJ ..........: " + pj.cnpj);
                Console.WriteLine("IE ............: " + pj.ie);
            }
            Console.WriteLine("Valor de Compra: " + c.valor.ToString("C"));
            Console.WriteLine("Imposto .......: " + c.valor_imposto.ToString("C"));
            Console.WriteLine("Total a Pagar .: " + c.total.ToString("C"));
        }
    }
}
