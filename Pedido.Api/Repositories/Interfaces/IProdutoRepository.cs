using PedidoDeVenda.Entities;

namespace PedidoDeVenda.Repositories.Interfaces
{
    public interface IProdutoRepository 
    {
        void CriarProduto(Produto produto);
        void RemoverProduto(int id);
        void AlteraNomeProduto(int id, string nome);
        Produto BuscaPorId(int id);
        List<Produto> ListarTodos();
    }
}
