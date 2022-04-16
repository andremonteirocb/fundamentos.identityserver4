using AutoMapper;
using Fundamentos.IS4.Loja.Domain.Models;

namespace Fundamentos.IS4.Loja.Data.Mappers
{
    internal static class EnderecoMapper
    {
        internal static IMapper Mapper { get; }

        static EnderecoMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<EnderecoMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Endereco ToModel(this Entities.Endereco entity)
        {
            return Mapper.Map<Endereco>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Endereco ToEntity(this Endereco model)
        {
            return Mapper.Map<Entities.Endereco>(model);
        }

    }
}