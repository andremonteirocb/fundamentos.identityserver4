using  Fundamentos.IS4.Loja.Domain.Interfaces;
using  Fundamentos.IS4.Loja.Domain.Models;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoStore _pedidoStore;

        public PedidoService(IPedidoStore pedidoStore)
        {
            _pedidoStore = pedidoStore;
        }

        public Task SalvarPedido(Pedido pedido, string usuario)
        {
            return _pedidoStore.SalvarPedido(pedido, usuario);
        }
    }
}
