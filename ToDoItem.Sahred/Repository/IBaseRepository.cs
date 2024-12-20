using System.Linq.Expressions;

namespace ToDoItem.Sahred.Repository
{
    public interface IBaseRepository<TEntity, TIdentitfier>
    {
        public void Insert(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

        public void Deleet(TIdentitfier id);

        public TEntity GetById(TIdentitfier id);

        public bool Any(Expression<Func<TEntity, bool>> expression);

        public TEntity GetByCondition(Expression<Func<TEntity, bool>> expression = default!);


        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
