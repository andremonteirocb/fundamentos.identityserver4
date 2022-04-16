using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;
using Carrinho =  Fundamentos.IS4.Loja.Domain.Models.Carrinho;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class CarrinhoMapperProfile : Profile
    {
        public CarrinhoMapperProfile()
        {
            CreateMap<Entities.Carrinho, Carrinho>()
                .ForMember(m => m.Items, opt => opt.MapFrom(o => o.CarrinhoProdutos));

            CreateMap<Carrinho, Entities.Carrinho>()
                .ForMember(m => m.CarrinhoProdutos, opt => opt.MapFrom(o => o.Items));

            CreateMap<ItemCarrinho, Entities.ItemCarrinho>()
                .ForMember(m => m.NomeProduto, opt => opt.MapFrom(m => m.Produto))
                .ReverseMap();

        }
    }
}