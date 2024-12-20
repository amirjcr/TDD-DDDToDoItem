using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Sahred.Repository;

namespace ToDoItem.Domain.ToDoAgg.Repository
{
    public interface IToDoItemRepository : IBaseRepository<ToDoAgg.ToDoItem, int>
    {
    }
}
