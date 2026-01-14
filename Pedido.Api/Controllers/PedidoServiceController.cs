using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidoDeVenda.Entities;
using PedidoDeVenda.Entities.Exceptions;
using PedidoDeVenda.Repositories.Interfaces;
using PedidoDeVenda.Services.Interfaces;

namespace PedidoDeVenda.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoServiceController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoServiceController(IPedidoService pedidoRepository)
        {
            _pedidoService = pedidoRepository;
        }

        [HttpGet]
        public ActionResult<List<Pedido>> ListarTodos()
        {
            try
            {
                var pedidos = _pedidoService.ListarTodos();

                return Ok(pedidos);
            }
            catch (DomainException)
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var pedido = _pedidoService.BuscarPorId(id);

                return Ok(pedido);
            }
            catch (DomainException)
            {
                return NoContent();
            }
        }

        //[HttpPost]
        //public IActionResult CriarPedido(int id, Pedido pedido)
        //{

        //}

    }
}
