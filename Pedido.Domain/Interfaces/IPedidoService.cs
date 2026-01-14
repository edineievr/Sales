using PedidoDeVenda.Entities;

namespace Pedido.Domain.Interfaces
{
    public interface IPedidoService 
    {
        void CriarPedido(int id);
        void RemoverPedido(int id);
        List<Pedido> ListarTodos();
        public Pedido BuscarPorId(int id);
        void AdicionarItem(int idPedido, ItemPedido item);
        void RemoverItem(int idPedido, int idItem);

    }
}
