using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal static class CategoriaMapper
    {
        internal static IMapper Mapper { get; }
        static CategoriaMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<CategoriaMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Categoria ToModel(this Entities.Categoria entity)
        {
            return Mapper.Map<Categoria>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Categoria ToEntity(this Categoria model)
        {
            return Mapper.Map<Entities.Categoria>(model);
        }

    }
}