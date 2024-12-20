using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Sahred.Repository;

namespace ToDoItem.Domain.UserAgg.Repository
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        public bool CanUserCreateItem(int userId);
    }
}
