using System;
using AutoMapper;
using EF.CodeFirst.Common.Domain;
using EF.CodeFirst.Common.Repository;
using EF.CodeFirst.Common.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace EF.CodeFirst.Common.Extensions
{
    public static class MapModelToEntityExtensions
    {
        public static TEntity MapFromEditModel<TEditModel, TEntity>(this TEditModel editModel, Action<TEditModel, TEntity> mapper = null)
           where TEditModel : class, IEditModel
           where TEntity : class, IEntity
        {
            var repository = ServiceLocator.Current.GetInstance<IRepository<TEntity>>();

            TEntity entity = repository.FindById(editModel.Id);

            if (mapper == null)
            {
                entity = Mapper.Map(editModel, entity);
            }
            else
            {
                mapper(editModel, entity);
            }

            return entity;
        }

        public static TEntity MapFromCreateModel<TCreateModel, TEntity>(this TCreateModel createModel, Func<TCreateModel, TEntity> mapper = null)
            where TCreateModel : class, ICreateModel
            where TEntity : class, IEntity, new()
        {
            TEntity entity = (mapper == null) ? Mapper.Map<TCreateModel, TEntity>(createModel) : mapper(createModel);

            return entity;
        }
    }
}
