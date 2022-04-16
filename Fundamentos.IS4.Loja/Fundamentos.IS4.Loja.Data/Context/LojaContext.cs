using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Entities;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Data.Context
{
    public class LojaContext : DbContext, IDataProtectionKeyContext
    {

        public LojaContext(DbContextOptions<LojaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureContext();
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        internal DbSet<Produto> Produtos { get; set; }
        internal DbSet<Categoria> Categorias { get; set; }
        internal DbSet<Avaliacao> Avaliacoes { get; set; }
        internal DbSet<Marca> Marcas { get; set; }
        internal DbSet<Carrinho> Carrinho { get; set; }
        internal DbSet<ItemCarrinho> CarrinhoProdutos { get; set; }
        internal DbSet<Endereco> Enderecos { get; set; }
        internal DbSet<Pedido> Pedidos { get; set; }
        internal DbSet<ProdutoVendido> ProdutosVendido { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }
    }
}