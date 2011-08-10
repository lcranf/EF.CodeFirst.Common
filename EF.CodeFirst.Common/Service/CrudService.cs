using System;
using System.Linq;
using System.Linq.Expressions;
using EF.CodeFirst.Common.Domain;
using EF.CodeFirst.Common.Extensions;
using EF.CodeFirst.Common.Repository;
using EF.CodeFirst.Common.ViewModels;

namespace EF.CodeFirst.Common.Service
{
    public class CrudService<TEntity> : ICrudService<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected IRepository<TEntity> Repository { get; set; }

        public CrudService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual TEntity Update(TEntity entity)
        {
            var updatedEntity = Repository.Update(entity);
            Repository.Save();

            return updatedEntity;
        }

        public virtual TEntity UpdateFromModel<TEditModel>(TEditModel editModel)
            where TEditModel : class, IEditModel
        {
            var entity = editModel.MapFromEditModel<TEditModel, TEntity>();

            return Update(entity);
        }

        public virtual TEntity Create(TEntity entity)
        {
            var createdEntity = Repository.Create(entity);
            Repository.Save();

            return createdEntity;
        }

        public virtual TEntity CreateFromModel<TCreateModel>(TCreateModel createModel)
            where TCreateModel : class, ICreateModel
        {
            var entity = createModel.MapFromCreateModel<TCreateModel, TEntity>();

            return Create(entity);
        }

        public virtual TEntity CreateOrUpdate(TEntity entity)
        {
            var createdOrUpdatedEntity = Repository.CreateOrUpdate(entity);
            Repository.Save();

            return createdOrUpdatedEntity;
        }

        public virtual bool Delete(int id)
        {
            var deleteSuccessful = Repository.Delete(id);
            Repository.Save();

            return deleteSuccessful;
        }

        public virtual TEntity FindById(int id)
        {
            return Repository.FindById(id);
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            return Repository.QueryBy();
        }

        public virtual IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> criteria = null)
        {
            var queryable = Repository.QueryBy();

            return criteria != null ? queryable.Where(criteria) : queryable;
        }

        public IQueryable<TEntity> QueryByIncludeProperties(params Expression<Func<TEntity, object>>[] includProperties)
        {
            return Repository.QueryByIncluding(includProperties);
        }
    }
}