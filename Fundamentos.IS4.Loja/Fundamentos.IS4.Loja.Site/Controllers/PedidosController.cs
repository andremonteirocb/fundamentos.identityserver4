using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Site.Controllers
{
    [Route("pedidos"), Authorize]
    public class PedidosController : Controller
    {
        private readonly IPedidoStore _pedidoStore;

        public PedidosController(IPedidoStore pedidoStore)
        {
            _pedidoStore = pedidoStore;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoStore.ListarPedidos(User.Identity.Name);
            return View(pedidos);
        }

        [Route("{identificador}")]
        public async Task<IActionResult> Detalhes(string identificador)
        {
            var pedido = await _pedidoStore.ObterPorIdentificador(identificador, User.Identity.Name);
            return View(pedido);
        }
    }
}
