using Bogus;
using Microsoft.Extensions.Configuration;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Data.Util
{
    public class FakeData
    {


        public static async Task<List<Produto>> CarregarProdutos(LojaContext context, string root)
        {
            var faker = new Faker("pt_BR");

            var marcas = Marca.Obter().Generate(13).Select(s => s.ToEntity()).ToList();
            await context.Marcas.AddRangeAsync(marcas);
            await context.SaveChangesAsync();

            var dir = new DirectoryInfo(Path.Combine(root, "images", "product"));
            var imagens = dir.GetFiles("*.jpg").Select(s => $"/images/product/{s.Name}");

            var produtos = Produto.Obter(imagens).Generate(1000);
            var produtosDb = produtos.Select(s => s.ToEntity().AtualizarMarca(faker.PickRandom(marcas)));
            await context.Produtos.AddRangeAsync(produtosDb);
            await context.SaveChangesAsync();

            return produtos;
        }

        public static async Task CarregarCategorias(LojaContext context, List<Produto> produtos)
        {
            var categorias = produtos.SelectMany(s => s.Categorias).Distinct();
            var categoriasParaSalvar = categorias.Select(s => Categoria.Obter(s).Generate().ToEntity());
            await context.Categorias.AddRangeAsync(categoriasParaSalvar);
            await context.SaveChangesAsync();
        }

        public static async Task PopularCarrinho(List<Produto> produtos, ICarrinhoService carrinhoService, string usuario)
        {
            var faker = new Faker();
            var produtosCarrinho = faker.PickRandom(produtos, 5);
            foreach (var produto in produtosCarrinho)
            {
                await carrinhoService.AdicionarProduto(usuario, produto, faker.Random.Int(1, 3));
            }
        }

        public static async Task GerarEnderecos(LojaContext context, IConfiguration configuration)
        {
            var usuario = configuration.GetValue<string>("ApplicationSettings:DefaultUser") ?? "bob";
            var faker = new Faker();
            var enderecos = Endereco.Obter(usuario)
                .Generate(faker.Random.Int(3, 7));

            var enderecosDb = enderecos.Select(s => s.ToEntity().AtualizarUsuario(usuario));
            await context.Enderecos.AddRangeAsync(enderecosDb);
            await context.SaveChangesAsync();
        }

        public static async Task GerarPedidos(LojaContext context, IConfiguration configuration, string root)
        {
            var usuario = configuration.GetValue<string>("ApplicationSettings:DefaultUser") ?? "bob";
            var dir = new DirectoryInfo(Path.Combine(root, "images", "product"));
            var imagens = dir.GetFiles("*.jpg").Select(s => $"/images/product/{s.Name}");

            var pedidos = Pedido.Obter(usuario, imagens).Generate(10);

            await context.Pedidos.AddRangeAsync(pedidos.Select(s => s.ToEntity().AtualizarUsuario(usuario)));
            await context.SaveChangesAsync();
        }
    }
}
