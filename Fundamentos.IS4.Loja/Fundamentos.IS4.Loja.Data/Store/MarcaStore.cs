using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Data.Store
{
    public class MarcaStore : IMarcaStore
    {
        internal DbSet<Entities.Marca> DbSet { get; set; }
        public MarcaStore(LojaContext context)
        {
            this.DbSet = context.Marcas;
        }

        public async Task<IEnumerable<Marca>> ObterTodos()
        {
            var marcas = await DbSet.ToListAsync();
            return marcas.Select(s => s.ToModel());
        }

        public async Task<Marca> ObterPorNome(string marca)
        {
            var marcaDb = await DbSet.FirstOrDefaultAsync(f => f.NomeUnico.Equals(marca));
            return marcaDb.ToModel();
        }
    }
}
