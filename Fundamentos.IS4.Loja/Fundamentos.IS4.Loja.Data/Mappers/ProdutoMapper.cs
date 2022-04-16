using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal static class ProdutoMapper
    {
        internal static IMapper Mapper { get; }
        static ProdutoMapper()
        {
            Mapper = new MapperConfiguration(m =>
                {
                    m.AddProfile<ProdutoMapperProfile>();
                    m.AddProfile<AvaliacaoMapperProfile>();
                    m.AddProfile<MarcaMapperProfile>();
                }
            ).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Produto ToModel(this Entities.Produto entity)
        {
            return Mapper.Map<Produto>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Produto ToEntity(this Produto model)
        {
            return Mapper.Map<Entities.Produto>(model);
        }
    }
}
