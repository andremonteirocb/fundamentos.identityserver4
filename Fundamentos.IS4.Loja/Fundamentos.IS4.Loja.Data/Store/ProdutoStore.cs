using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using Fundamentos.IS4.Loja.Data.Util;
using Fundamentos.IS4.Loja.Domain.Interfaces;
using Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentos.IS4.Loja.Domain.Extensions;

namespace Fundamentos.IS4.Loja.Data.Store
{
    public class ProdutoStore : IProdutoStore
    {
        private DbSet<Entities.Produto> DbSet { get; set; }
        private LojaContext Db { get; set; }

        public ProdutoStore(LojaContext context)
        {
            this.Db = context;
            this.DbSet = context.Produtos;
        }

        public async Task<List<Produto>> ObterTodos()
        {
            var produtos = await DbSet.AsNoTracking().ToListAsync();

            return produtos.Select(s => s.ToModel()).ToList();
        }

        public async Task<Produto> ObterPorNome(string nomeUnico)
        {
            var produto = await Obter(nomeUnico);
            return produto.ToModel();
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(string categoria)
        {
            var produts = await DbSet.Where(w => w.Categorias.ToLower().Contains(categoria.ToLower())).ToListAsync();
            return produts.Select(s => s.ToModel());
        }

        public async Task<ListOf<Produto>> PesquisarPorCategoria(PesquisarProdutoVo model)
        {
            var query = DbSet.Include(i => i.Marca).Filter(model);

            var produtos = await query.OrderBy(o => o.Valor).Paginate(model).ToListAsync();
            var quantidade = await query.CountAsync();

            return new ListOf<Produto>(produtos.Select(s => s.ToModel()), quantidade);
        }

        public async Task<ListOf<Produto>> PesquisarPorMarca(PesquisarProdutoVo model)
        {
            var query = DbSet.Include(i => i.Marca).Filter(model);

            var produtos = await query.OrderBy(o => o.Valor).Paginate(model).ToListAsync();
            var quantidade = await query.CountAsync();

            return new ListOf<Produto>(produtos.Select(s => s.ToModel()), quantidade);
        }

        public async Task<ListOf<Produto>> Pesquisar(PesquisarProdutoVo model)
        {
            var query = DbSet.Include(i => i.Marca).Filter(model);

            if (model.Query.IsPresent())
                query = query.Where(w =>
                        w.Nome.ToLower().Contains(model.Query.ToLower()) ||
                        w.Detalhes.ToLower().Contains(model.Query.ToLower()) ||
                        w.Descricao.ToLower().Contains(model.Query.ToLower()) ||
                        w.Categorias.ToLower().Contains(model.Query.ToLower()) ||
                        w.Marca.Nome.ToLower().Contains(model.Query.ToLower()));


            var produtos = await query.OrderBy(o => o.Valor).Paginate(model).ToListAsync();
            var quantidade = await query.CountAsync();
            return new ListOf<Produto>(produtos.Select(s => s.ToModel()), quantidade);
        }

        private async Task<Entities.Produto> Obter(string nomeUnico)
        {
            var produto = await DbSet
                .Include(i => i.Avaliacoes)
                .Include(i => i.Marca).AsNoTracking()
                .FirstOrDefaultAsync(f => f.NomeUnico.ToLower().Equals(nomeUnico));
            return produto;
        }

        public async Task Adicionar(Produto obj)
        {
            await DbSet.AddAsync(obj.ToEntity());
            await Db.SaveChangesAsync();
        }

        public async Task Atualizar(Produto obj)
        {
            var entityDb = await Obter(obj.NomeUnico);
            var entity = obj.ToEntity();

            entity.Id = entityDb.Id;
            entity.ShallowCopyTo(entityDb);

            await Db.SaveChangesAsync();
        }

        public async Task Remover(Produto obj)
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
