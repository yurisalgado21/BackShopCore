using BackShopCore.Data;
using BackShopCore.Utils;
using Microsoft.EntityFrameworkCore;

namespace BackShopCore.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<TEntity> _dbSetEntity;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetEntity = _dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _dbSetEntity.Add(entity: entity);

            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSetEntity.AddRange(entities: entities);
        }

        public void Delete(int id)
        {
            _dbSetEntity.Remove(entity: _dbSetEntity.Find(id)!);
        }

        public IQueryable<TEntity> GetAll(PaginationFilter paginationFilter)
        {
            var pagedData = _dbSetEntity
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);

            return pagedData;
        }

        public TEntity GetById(int id)
        {
            return _dbSetEntity.Find(keyValues: id)!;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public TEntity Update(int id, TEntity entity)
        {
            _dbSetEntity.Update(entity: entity);

            return _dbSetEntity.Find(keyValues: id)!;
        }
    }
}