using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using Fundamentos.IS4.Loja.Data.Util;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Store
{
    public class CategoriaStore : ICategoriaStore
    {
        private LojaContext Db { get; set; }
        private DbSet<Entities.Categoria> DbSet { get; set; }

        public CategoriaStore(LojaContext context)
        {
            this.Db = context;
            this.DbSet = context.Categorias;
        }



        public async Task<List<Categoria>> ObterTodos()
        {
            var entity = await DbSet.OrderBy(b => b.Nome).ToListAsync();

            return entity.Select(s => s.ToModel()).ToList();
        }

        public async Task<Categoria> ObterPorNome(string nomeUnico)
        {
            var entity = await Obter(nomeUnico);
            return entity.ToModel();
        }


        private async Task<Entities.Categoria> Obter(string nomeUnico)
        {
            return await DbSet.FirstOrDefaultAsync(f => f.NomeUnico.ToLower().Equals(nomeUnico.ToLower()));
        }

        public async Task Adicionar(Categoria obj)
        {
            await DbSet.AddAsync(obj.ToEntity());
            await Db.SaveChangesAsync();
        }

        public async Task Atualizar(Categoria obj)
        {
            var entityDb = await Obter(obj.NomeUnico);
            var entity = obj.ToEntity();

            entity.Id = entityDb.Id;

            entity.ShallowCopyTo(entityDb);
            await Db.SaveChangesAsync();
        }

        public async Task Remover(Categoria obj)
        {
            var paraRemover = await Obter(obj.NomeUnico);
            DbSet.Remove(paraRemover);
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
        }

    }
}