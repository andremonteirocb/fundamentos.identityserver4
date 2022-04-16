using Microsoft.Extensions.DependencyInjection;
using Fundamentos.IS4.Loja.CrossCutting.Services;
using Fundamentos.IS4.Loja.Data.Store;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using Fundamentos.IS4.Loja.Domain.Services;
using Fundamentos.IS4.Loja.Site.Service;

namespace Fundamentos.IS4.Loja.Site.Configure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services)
        {
            services.AddScoped<IProdutoStore, ProdutoStore>();
            services.AddScoped<ICategoriaStore, CategoriaStore>();
            services.AddScoped<ICarrinhoStore, CarrinhoStore>();
            services.AddScoped<IMarcaStore, MarcaStore>();
            services.AddScoped<IAvaliacaoStore, AvaliacaoStore>();
            services.AddScoped<IEnderecoStore, EnderecoStore>();
            services.AddScoped<IPedidoStore, PedidoStore>();
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<IUserGeoLocation, GeoLocateUsers>();
            services.AddScoped<IGeoposicaoService, GeoposicaoService>();
            services.AddScoped<IFreteService, FreteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            return services;
        }
    }
}
