using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IEnderecoStore
    {
        Task<IEnumerable<Endereco>> ObterDoUsuario(string usuario);
    }
}
