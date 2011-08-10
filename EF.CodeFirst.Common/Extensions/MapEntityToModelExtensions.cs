using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EF.CodeFirst.Common.Domain;
using Simple.Extensions;

namespace EF.CodeFirst.Common.Extensions
{
    public static class MapEntityToModelExtensions
    {
        public static TModel MapTo<TEntity, TModel>(this TEntity entity, TModel dto)
            where TEntity : IEntity
            where TModel : class
        {
            Mapper.Map(entity, dto, entity.GetType(), dto.GetType());

            return dto;
        }

        public static IEnumerable<TModel> MapListTo<TEntity, TModel>(this IEnumerable<TEntity> entities, TModel dto)
            where TEntity : IEntity
            where TModel : class
        {
            if (entities.IsNullOrEmpty()) return Enumerable.Empty<TModel>();

            var dtos = new List<TModel>();

            Mapper.Map(entities, dtos);

            return dtos;
        }


    }
}
