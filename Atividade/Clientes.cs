using System;

namespace Atividade
{
    // Classe-pai: ABSTRAÇÃO do que todo cliente tem em comum (diagrama: Clientes).
    class Clientes
    {
        // + (público) no diagrama
        public string nome { get; set; }
        public string endereco { get; set; }

        // # (protegido) no diagrama -> ENCAPSULAMENTO:
        // qualquer um pode ler, mas só a própria classe e as filhas podem alterar.
        public float valor { get; protected set; }
        public float valor_imposto { get; protected set; }
        public float total { get; protected set; }

        // virtual: permite que a classe filha reescreva o método (POLIMORFISMO).
        // Regra padrão: imposto de 10% (pessoa física).
        public virtual void Pagar_Imposto(float v)
        {
            ValidarValor(v);
            this.valor = v;
            this.valor_imposto = this.valor * 10 / 100;
            this.total = this.valor + this.valor_imposto;
        }

        // Regra de negócio protegida: não aceita valor zero ou negativo.
        protected void ValidarValor(float v)
        {
            if (v <= 0)
                throw new ArgumentException("O valor de compra deve ser maior que zero.");
        }
    }
}
