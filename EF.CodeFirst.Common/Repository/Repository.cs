using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EF.CodeFirst.Common.Db;
using EF.CodeFirst.Common.Domain;

namespace EF.CodeFirst.Common.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected DbContext DbContext;
        protected DbSet<TEntity> DbSetOfEntities;

        public Repository(IDbContextRegistry registry)
        {
            DbContext = registry.CurrentContext;
            DbSetOfEntities = DbContext.Set<TEntity>();
        }

        public IEntity Attach(IEntity entity)
        {
            DbSetOfEntities.Attach((TEntity) entity);

            return entity;
        }

        public TEntity Create(TEntity entity)
        {
            return DbSetOfEntities.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public TEntity CreateOrUpdate(TEntity entity)
        {
            return entity.Id == default(int) ? Create(entity) : Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            return DbSetOfEntities.Remove(entity) != null;
        }

        public bool Delete(int entityId)
        {
            //per stack overflow:  http://bit.ly/pI8Dyi
            //The best way to delete an entity and associated children is the following,
            //which will require ONE round trip to the server, thus preventing N+1 queries
            //NOTE:  For this to properly work, the many-side of the relationship must have
            //a non-nullable association with it's parent
            var entity = DbSetOfEntities.Attach(new TEntity { Id = entityId });

            return Delete(entity);
        }

        public IQueryable<TEntity> QueryBy()
        {
            return DbSetOfEntities;
        }

        public IQueryable<TEntity> QueryByIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSetOfEntities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public TEntity FindById(int entityId)
        {
            return DbSetOfEntities.Find(entityId);
        }

        public IEntity GetById(int entityId)
        {
            return DbSetOfEntities.Find(entityId);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return DbSetOfEntities;
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> where)
        {
            return DbSetOfEntities.Where(where);
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
