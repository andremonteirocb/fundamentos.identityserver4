using AutoMapper;
using  Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class EnderecoMapperProfile : Profile
    {
        public EnderecoMapperProfile()
        {
            CreateMap<Entities.Endereco, Endereco>().ReverseMap();
        }
    }
}