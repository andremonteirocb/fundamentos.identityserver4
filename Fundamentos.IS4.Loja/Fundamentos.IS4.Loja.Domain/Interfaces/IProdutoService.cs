using  Fundamentos.IS4.Loja.Domain.Models;
using System.Threading.Tasks;

namespace  Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task Comentar(Avaliacao avaliacao);
    }
}