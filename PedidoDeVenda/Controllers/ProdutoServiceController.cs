using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedidoDeVenda.Services;
using PedidoDeVenda.Entities;
using PedidoDeVenda.Repositories;
using PedidoDeVenda.Services.Interfaces;
using PedidoDeVenda.Entities.Exceptions;

namespace PedidoDeVenda.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoServiceController : ControllerBase
    {
        private readonly IProdutoService _produtos;

        public ProdutoServiceController(IProdutoService pedidoService)
        {
            _produtos = pedidoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ListarTodos()
        {
            var produtos = _produtos.ListarTodos();

            if (produtos == null)
            {
                return NoContent();
            }

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var p = _produtos.BuscaPorId(id);

                return Ok(p);
            }
            catch (DomainException)
            {
                return NotFound("Produto não encontrado.");
            }
        }

        [HttpPost]
        public ActionResult CriaProduto(Produto produto)
        {
            try
            {
                _produtos.CriarProduto(produto);

                return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id}, produto);
            }
            catch (DomainException)
            {
                return BadRequest("Não é possível gravar um produto vazio.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarProduto(int id)
        {
            try
            {
                var p = _produtos.BuscaPorId(id);

                _produtos.RemoverProduto(id);

                return NoContent();

            }catch (DomainException)
            {
                return NotFound("Produto não encontrado");

            }
        }

        [HttpPatch("{id}/nome")]
        public ActionResult AtualizaNomeProduto(int id, string nomeAtualizado)
        {
            try
            {
                var p = _produtos.BuscaPorId(id);

                if (!string.IsNullOrEmpty(nomeAtualizado))
                {
                    p.AtualizaNomeProduto(nomeAtualizado);

                    return NoContent();
                }
                else
                {
                    return BadRequest("Nome do produto não pode ser vazio");
                }
            }
            catch (DomainException)
            {
                return NotFound("Produto não encontrado");
            }
        }

        [HttpPatch("{id}/preco")]
        public ActionResult AtualizaPrecoProduto(int id, decimal precoAtualizado)
        {
            try
            {
                var p = _produtos.BuscaPorId(id);

                if (precoAtualizado < 0)
                {
                    return BadRequest("Preço do produto não pode ser menor que 0");
                }

                p.AtualizaValorProduto(precoAtualizado);

                return Ok(p);
            }
            catch (DomainException)
            {
                return NotFound("Produto não encontrado.");
            }
        }

    }
}
