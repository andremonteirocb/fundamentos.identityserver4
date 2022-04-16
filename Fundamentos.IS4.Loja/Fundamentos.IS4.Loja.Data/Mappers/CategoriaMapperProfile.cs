using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class CategoriaMapperProfile : Profile
    {
        public CategoriaMapperProfile()
        {
            CreateMap<Entities.Categoria, Categoria>().ReverseMap();
        }
    }
}