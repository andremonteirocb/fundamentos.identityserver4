using System.Threading.Tasks;
using  Fundamentos.IS4.Loja.Domain.Models;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IPedidoService
    {
        Task SalvarPedido(Pedido pedido, string usuario);
    }
}