using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;
using System;
using System.Linq;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class ProdutoMapperProfile : Profile
    {
        public ProdutoMapperProfile()
        {

            CreateMap<Entities.Produto, Produto>()
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(m => m.Categorias.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Cores, opt => opt.MapFrom(m => m.Cores.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Imagens, opt => opt.MapFrom(m => m.Imagens.Split(";", StringSplitOptions.RemoveEmptyEntries).ToHashSet()))
                .ForMember(dest => dest.Avaliacoes, opt => opt.MapFrom(m => m.Avaliacoes.OrderByDescending(b => b.DataAvaliacao)));

            CreateMap<Produto, Entities.Produto>()
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(m => string.Join(";", m.Categorias)))
                .ForMember(dest => dest.Cores, opt => opt.MapFrom(m => string.Join(";", m.Cores)))
                .ForMember(dest => dest.Imagens, opt => opt.MapFrom(m => string.Join(";", m.Imagens)))
                .ForMember(dest => dest.Avaliacoes, opt => opt.MapFrom(m => m.Avaliacoes));

        }
    }
}