using System;
using AutoMapper;
using EF.CodeFirst.Common.Domain;
using EF.CodeFirst.Common.Repository;
using Microsoft.Practices.ServiceLocation;

namespace EF.CodeFirst.Common.Mappings
{
    public class IdToEntityTypeConverter<TEntity> : TypeConverter<int, TEntity>
        where TEntity : IEntity, new()
    {
        protected override TEntity ConvertCore(int entityId)
        {
            Type repositoryType = typeof(IRepository<>).MakeGenericType(typeof(TEntity));

            var repository = (IAttachableRepository)ServiceLocator.Current.GetInstance(repositoryType);

            return (TEntity) repository.Attach(new TEntity { Id = entityId });
        }
    }
}