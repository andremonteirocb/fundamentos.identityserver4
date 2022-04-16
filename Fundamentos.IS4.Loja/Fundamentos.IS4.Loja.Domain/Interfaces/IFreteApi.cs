using Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Domain.Interfaces
{
    public interface IFreteApi
    {
        [Get("/fretes/para/{lat},{lon}/calcular")]
        Task<IEnumerable<Frete>> Calcular([AliasAs("lat")] double latitude, [AliasAs("lon")] double longitude, Embalagem embalagem, [Header("Authorization")] string token);

        [Get("/fretes")]
        Task<IEnumerable<DetalhesFrete>> Modalidades([Header("Authorization")] string token);
    }
}