using PedidoDeVenda.Entities;

namespace PedidoDeVenda.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void AdicionarItem(int idPedido, ItemPedido item);
        void RemoverItem(int idPedido, int idItem);
        void CriarPedido(Pedido pedido);
        List<Pedido> ListarTodos();
        void RemoverPedido(int id);
        Pedido BuscaPorId(int id);
    }
}
