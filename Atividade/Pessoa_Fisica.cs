namespace Atividade
{
    // HERANÇA: Pessoa_Fisica herda nome, endereco, valores e Pagar_Imposto (10%) de Clientes.
    class Pessoa_Fisica : Clientes
    {
        public string cpf { get; set; }
        public string rg { get; set; }
    }
}
