using AutoMapper;
using Fundamentos.IS4.Loja.Data.Entities;
using Marca =  Fundamentos.IS4.Loja.Domain.Models.Marca;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class MarcaMapperProfile : Profile
    {
        public MarcaMapperProfile()
        {
            CreateMap<Entities.Marca, Marca>().ReverseMap();
        }
    }
}