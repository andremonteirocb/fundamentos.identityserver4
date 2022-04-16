using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using  Fundamentos.IS4.Loja.Domain.Models;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Data.Store
{
    public class AvaliacaoStore : IAvaliacaoStore
    {
        internal LojaContext Db { get; set; }
        internal DbSet<Entities.Avaliacao> DbSet { get; set; }
        internal DbSet<Entities.Produto> ProdutosDbSet { get; set; }
        public AvaliacaoStore(LojaContext context)
        {
            this.Db = context;
            this.DbSet = context.Avaliacoes;
            this.ProdutosDbSet = context.Produtos;
        }


        public async Task SalvarAvaliacao(Avaliacao avaliacao)
        {
            var entity = avaliacao.ToEntity();
            var produto = await ProdutosDbSet.Include(i => i.Avaliacoes).FirstOrDefaultAsync(f => f.NomeUnico.ToLower().Equals(avaliacao.ProdutoUrl));
            produto.Avaliacoes.Add(entity);

            await Db.SaveChangesAsync();
        }
    }
}
