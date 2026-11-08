namespace Atividade
{
    // HERANÇA: Pessoa_Juridica também é um Cliente.
    class Pessoa_Juridica : Clientes
    {
        public string cnpj { get; set; }
        public string ie { get; set; }

        // POLIMORFISMO: reescreve (override) o método da classe-pai com imposto de 20%.
        public override void Pagar_Imposto(float v)
        {
            ValidarValor(v);
            this.valor = v;
            this.valor_imposto = this.valor * 20 / 100;
            this.total = this.valor + this.valor_imposto;
        }
    }
}
