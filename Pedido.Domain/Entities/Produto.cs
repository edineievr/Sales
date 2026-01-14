using PedidoDeVenda.Entities.Exceptions;

namespace PedidoDeVenda.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Saldo { get; private set; }
        public decimal Preco { get; private set; }

        public Produto()
        {

        }

        public Produto(int id, string nome, int saldo, decimal preco)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome não pode ser vazio.");
            }

            if (saldo < 0)
            {
                throw new DomainException("Quantidade não pode ser menor que 0.");
            }

            if (preco < 0)
            {
                throw new DomainException("Preço não pode ser negativo.");
            }

            Id = id;
            Nome = nome;
            Saldo = saldo;
            Preco = preco;
        }

        public void AdicionaSaldo(int saldo)
        {
            if (saldo < 0)
            {
                throw new DomainException("Não é possível adicionar quantidade negativa.");
            }

            Saldo += saldo;
        }

        public void RemoveSaldo(int saldo)
        {
            if (saldo <= 0)
            {
                throw new DomainException("Quantidade para remover deve ser maior que 0.");
            }

            if (Saldo < saldo)
            {
                throw new DomainException("Saldo insuficiente.");
            }

            Saldo -= saldo;
            
        }

        public void AtualizaNomeProduto(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome não pode ser vazio.");
            }

            Nome = nome;
        }

        public void AtualizaValorProduto(decimal preco)
        {
            if (preco < 0)
            {
                throw new DomainException("Preço não pode ser negativo.");
            }

            Preco = preco;
        }


    }
}
