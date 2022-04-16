using System.Collections.Generic;
using System.Threading.Tasks;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface ICategoriaStore : IStore<Categoria>
    {
        Task<List<Categoria>> ObterTodos();
        Task<Categoria> ObterPorNome(string nomeUnico);
    }
}