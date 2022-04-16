using Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IProdutoStore : IStore<Models.Produto>
    {
        Task<List<Models.Produto>> ObterTodos();
        Task<Models.Produto> ObterPorNome(string nomeUnico);
        Task<IEnumerable<Models.Produto>> ObterPorCategoria(string categoria);
        Task<ListOf<Produto>> Pesquisar(PesquisarProdutoVo model);
        Task<ListOf<Produto>> PesquisarPorCategoria(PesquisarProdutoVo model);
        Task<ListOf<Produto>> PesquisarPorMarca(PesquisarProdutoVo model);
    }
}
