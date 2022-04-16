using  Fundamentos.IS4.Loja.Domain.Models;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Site.Service
{
    public interface IGeoposicaoService
    {
        Task<string> GetRequestIp(bool tryUseXForwardHeader = true);
        Task<LocalizacaoAtual> ObterLocalizacaoAtual();
        Task<GeoCoordinate> GeolocalizarUsuario();
    }
}