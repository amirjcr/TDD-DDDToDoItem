using Microsoft.EntityFrameworkCore;

namespace ToDoItem.Sahred.Repository
{
    public interface IDBContext
    {
        public DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        public DbSet<TEntity> Set<TEntity>(string name)
            where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
