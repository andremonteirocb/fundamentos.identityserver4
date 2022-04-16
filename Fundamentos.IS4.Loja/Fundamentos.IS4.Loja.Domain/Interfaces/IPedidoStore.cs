using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IPedidoStore
    {
        Task SalvarPedido(Pedido pedido, string usuario);
        Task<Pedido> ObterPorIdentificador(string identificador, string usuario);
        Task<IEnumerable<Pedido>> ListarPedidos(string usuario);
        Task AtualizarStatus(string identificador, string usuario, StatusPedido status);
    }
}