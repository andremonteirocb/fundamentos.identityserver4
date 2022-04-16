using  Fundamentos.IS4.Loja.Domain.Models;
using System.Threading.Tasks;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface ICarrinhoStore
    {
        Task<Carrinho> ObterCarrinho(string usuario);
        Task<Carrinho> CriarCarrinho(string usuario);
        Task AdicionarItemAoCarrinho(Carrinho carrinho, ItemCarrinho item);
        Task AtualizarItemCarrinho(ItemCarrinho item, Carrinho carrinho);
        Task Remover(string produto, string user);
        Task AtualizarCarrinho(Carrinho carrinho);
        Task RemoverTodosItens(string usuario);
    }
}