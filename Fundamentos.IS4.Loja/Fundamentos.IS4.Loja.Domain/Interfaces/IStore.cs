using System;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IStore<TEntity> : IDisposable
    {
        Task Adicionar(TEntity obj);
        Task Atualizar(TEntity obj);
        Task Remover(TEntity obj);
    }
}