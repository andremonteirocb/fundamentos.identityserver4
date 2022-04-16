namespace Fundamentos.IS4.Frete.Fretes.Context
{
    public static class ModelBuilder
    {

        /// <summary>
        /// Configures the client context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void ConfigureFreteContext(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Frete>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.Modalidade).IsUnique();
                e.Property(p => p.Descricao).HasMaxLength(1000);
            });
        }
    }
}
