using PedidoDeVenda.Repositories.Interfaces;
using PedidoDeVenda.Entities;
using PedidoDeVenda.Entities.Exceptions;
using Pedido.Domain.Interfaces;

namespace PedidoDeVenda.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void CriarProduto(Produto produto)
        {
            if(produto == null)
            {
                throw new DomainException("Produto não pode ser nulo.");
            }

            _produtoRepository.CriarProduto(produto);
        }

        public void RemoverProduto(int id)
        {
            var produto = BuscaPorId(id);

            if (produto == null)
            {
                throw new DomainException("Não localizado");
            }

            _produtoRepository.RemoverProduto(id);
            
        }

        public List<Produto> ListarTodos()
        {
            return _produtoRepository.ListarTodos();
        }

        public Produto BuscaPorId(int id)
        {
            var p = _produtoRepository.BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            return p;
        }

        public void AtualizaNomeProduto(int id, string nome)
        {
            var p = BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome não pode ser vazio.");
            }

            p.AtualizaNomeProduto(nome);
        }

        public void AtualizaPrecoProduto(int id, decimal precoAlterado)
        {
            var p = BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            if (precoAlterado < 0)
            {
                throw new DomainException("Valor não pode ser menor que 0.");
            }

            p.AtualizaValorProduto(precoAlterado);
        }


    }
}
