using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal class PedidoMapperProfile : Profile
    {
        public PedidoMapperProfile()
        {

            CreateMap<Pedido, Entities.Pedido>();

            CreateMap<Entities.Pedido, Pedido>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(o => o.Produtos));

            CreateMap<Entities.ProdutoVendido, SnapshotProduto>().ReverseMap();
        }
    }
}