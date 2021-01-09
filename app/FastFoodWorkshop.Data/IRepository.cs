namespace FastFoodWorkshop.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        void Update(TEntity entity);

        FastFoodWorkshopDbContext GetContext();
    }
}
