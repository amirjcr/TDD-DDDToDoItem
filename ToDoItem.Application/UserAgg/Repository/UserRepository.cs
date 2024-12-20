
using System.Security.Cryptography.X509Certificates;
using ToDoItem.Application.Contexts;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Sahred.Repository;

namespace ToDoItem.Application.UserAgg.Repository
{
    public sealed class UserRepository : BaseRepository<User, int, IDatabaseContext>, IUserRepository
    {
        private readonly IDatabaseContext _context;
        public UserRepository(IDatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public bool CanUserCreateItem(int userId)
        {
            return _context.Users.
        }
    }
}
