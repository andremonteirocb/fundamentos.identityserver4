using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal static class AvaliacaoMapper
    {
        internal static IMapper Mapper { get; }

        static AvaliacaoMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<AvaliacaoMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Avaliacao ToModel(this Entities.Avaliacao entity)
        {
            return Mapper.Map<Avaliacao>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Avaliacao ToEntity(this Avaliacao model)
        {
            return Mapper.Map<Entities.Avaliacao>(model);
        }

    }
}