using Microsoft.EntityFrameworkCore;

namespace Fundamentos.IS4.Frete.Fretes.Context
{
    public class FreteContext : DbContext
    {
        public FreteContext(DbContextOptions<FreteContext> options)
            : base(options)
        {
        }

        public DbSet<Model.Frete> Fretes { get; set; }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureFreteContext();
        }
    }
}
