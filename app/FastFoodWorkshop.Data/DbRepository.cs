namespace FastFoodWorkshop.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly FastFoodWorkshopDbContext context;
        private DbSet<TEntity> dbSet;

        public DbRepository(FastFoodWorkshopDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public FastFoodWorkshopDbContext GetContext()
        {
            return this.context;
        }

        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
             this.dbSet.Update(entity);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
