using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Sahred.Repository
{
    public abstract class BaseRepository<TEntity, TIdentitfier, TConetxt> : IBaseRepository<TEntity, TIdentitfier>
        where TEntity : class
        where TConetxt : IDBContext
    {
        private readonly DbSet<TEntity> _db;
        private readonly IDBContext _context;
        protected BaseRepository(IDBContext context)
        {
            _context = context;
            _db = context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        => _db.Add(entity);

        public void Update(TEntity entity)
        => _db.Update(entity);

        public void Delete(TEntity entity)
         => _db.Remove(entity);

        public void Deleet(TIdentitfier id)
         => _db.Remove(GetById(id));

        public TEntity GetById(TIdentitfier id) => _db.Find(id)!;

        public TEntity GetByCondition(Expression<Func<TEntity, bool>> expression = default!)
        {
            if (expression is null) return default!;

            return _db.SingleOrDefault(expression)!;
        }

        public int SaveChanges() => _context.SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
    }
}
