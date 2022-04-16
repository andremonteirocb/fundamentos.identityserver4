using System.Collections.Generic;
using System.Threading.Tasks;
using  Fundamentos.IS4.Loja.Domain.Models;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IMarcaStore
    {
        Task<IEnumerable<Marca>> ObterTodos();
        Task<Marca> ObterPorNome(string marca);
    }
}
