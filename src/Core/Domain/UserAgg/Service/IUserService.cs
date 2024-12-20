using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Domain.UserAgg.Service
{
    public interface IUserService
    {
        bool CheckEmailIsDuuplicate(string email);
    }
}
