using AutoMapper;
using EF.CodeFirst.Common.Domain;

namespace EF.CodeFirst.Common.Mappings
{
    public class EntityToIdTypeConverter<TEntity> : TypeConverter<TEntity, int>
        where TEntity : IEntity
    {
        protected override int ConvertCore(TEntity entity)
        {
            return entity.Id;
        }
    }
}
