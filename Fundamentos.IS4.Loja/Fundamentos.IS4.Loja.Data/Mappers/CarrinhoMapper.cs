using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal static class CarrinhoMapper
    {
        internal static IMapper Mapper { get; }
        static CarrinhoMapper()
        {
            Mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<CarrinhoMapperProfile>();
                m.AddProfile<ProdutoMapperProfile>();
                m.AddProfile<AvaliacaoMapperProfile>();
                m.AddProfile<MarcaMapperProfile>();
            }).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Carrinho ToModel(this Entities.Carrinho entity)
        {
            return Mapper.Map<Carrinho>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Carrinho ToEntity(this Carrinho model)
        {
            return Mapper.Map<Entities.Carrinho>(model);
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static ItemCarrinho ToModel(this Entities.ItemCarrinho entity)
        {
            return Mapper.Map<ItemCarrinho>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.ItemCarrinho ToEntity(this ItemCarrinho model)
        {
            return Mapper.Map<Entities.ItemCarrinho>(model);
        }

    }
}