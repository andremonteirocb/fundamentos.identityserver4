using  Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IFreteService
    {
        Task<IEnumerable<Frete>> CalcularFrete(Embalagem embalagem, GeoCoordinate posicao, string token);
        Task<IEnumerable<Frete>> CalcularCarrinho(Carrinho carrinho, GeoCoordinate posicao, string token);
        Task<IEnumerable<DetalhesFrete>> ObterModalidades(string token);
    }
}