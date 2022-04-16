using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Fundamentos.IS4.Frete.Fretes.Context;
using Fundamentos.IS4.Frete.Site.Configure;
using System;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Frete.Fretes.Configure
{
    public class DatabaseChecker
    {
        public static async Task EnsureDatabaseIsReady(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<FreteContext>();

            Log.Information("Testing conection with database");
            await DbHealthChecker.TestConnection(context);
            Log.Information("Connection successfull");


            Log.Information("Aguarde, carregando fretes");
            if (!context.Fretes.Any())
            {
                await context.Fretes.AddRangeAsync(new[]
                {
                    new Model.Frete("Standard", "Entrega em até 15 dias uteis", 9,1),
                    new Model.Frete("Fast", "Entrega em até 7 dias uteis", 15,2m),
                    new Model.Frete("Ultra", "Entrega em até 2 dias uteis", 20, 1.5m)
                });

                await context.SaveChangesAsync();
            }



            Log.Information("Dados carregados");
        }
    }
}
