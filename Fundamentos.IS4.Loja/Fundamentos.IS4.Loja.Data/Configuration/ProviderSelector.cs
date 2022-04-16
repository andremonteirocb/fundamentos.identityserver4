using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Fundamentos.IS4.Loja.Data.Configuration.ProviderConfiguration;

namespace Fundamentos.IS4.Loja.Data.Configuration
{
    public static class ProviderSelector
    {
        public static IServiceCollection ConfigureProviderForContext<TContext>(
            this IServiceCollection services,
            (DatabaseType, string) options) where TContext : DbContext
        {
            var (database, connString) = options;
            Build(connString);
            return database switch
            {
                DatabaseType.SqlServer => services.PersistStore<TContext>(With.SqlServer),
                DatabaseType.MySql => services.PersistStore<TContext>(With.MySql),
                DatabaseType.Postgre => services.PersistStore<TContext>(With.Postgre),
                DatabaseType.Sqlite => services.PersistStore<TContext>(With.Sqlite),
                DatabaseType.InMemory => services.PersistStore<TContext>(With.InMemory),

                _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
            };
        }

        public static Action<DbContextOptionsBuilder> WithProviderAutoSelection((DatabaseType, string) options)
        {
            var (database, connString) = options;
            Build(connString);
            return database switch
            {
                DatabaseType.SqlServer => With.SqlServer,
                DatabaseType.MySql => With.MySql,
                DatabaseType.Postgre => With.Postgre,
                DatabaseType.Sqlite => With.Sqlite,
                DatabaseType.InMemory => With.InMemory,
                _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
            };
        }
    }
}