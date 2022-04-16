using AutoMapper;
using  Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class AvaliacaoMapperProfile : Profile
    {
        public AvaliacaoMapperProfile()
        {
            CreateMap<Entities.Avaliacao, Avaliacao>()
                .ForMember(dest => dest.ProdutoUrl, opt => opt.MapFrom(m => m.Produto.NomeUnico));

            CreateMap<Avaliacao, Entities.Avaliacao>();
        }
    }
}