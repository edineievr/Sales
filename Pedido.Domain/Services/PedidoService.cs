using Pedido.Domain.Interfaces;
using Pedido.Domain.Entities;
using Pedido.Domain.Entities.Exceptions;
using Pedido.Domain.Repositories.Interfaces;

namespace Pedido.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidos;

        public PedidoService(IPedidoRepository repository)
        {
            _pedidos = repository;
        }

        public void CriarPedido(int id)
        {
            var p = _pedidos.BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Pedido ja criado!");
            }
            else
            {
                _pedidos.CriarPedido(p);
            }
                
        }
        public void RemoverPedido(int id)
        {
            var p = _pedidos.BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Pedido não encontrado");
            }

            _pedidos.RemoverPedido(id);
        }

        public List<Pedido> ListarTodos()
        {
            var pedidos = _pedidos.ListarTodos();

            if (pedidos.Count == 0)
            {
                throw new DomainException("Não há pedidos para listagem");
            }

            return pedidos;
        }

        public Pedido BuscarPorId(int id)
        {
            var p = _pedidos.BuscaPorId(id);

            if (p == null)
            {
                throw new DomainException("Pedido não encontrado.");
            }

            return p;
        }

        public void AdicionarItem(int idPedido, ItemPedido item)
        {

            var p = _pedidos.BuscaPorId(idPedido);

            if (p == null)
            {
                throw new DomainException("Pedido não encontrado.");
            }

            p.AdicionaItem(item);
        }

        public void RemoverItem(int idPedido, int idItem)
        {
            var p = BuscarPorId(idPedido);

            if (p == null)
            {
                throw new DomainException("Pedido não encontrado.");
            }

            p.RemoveItem(idItem);
        }

    }
}

