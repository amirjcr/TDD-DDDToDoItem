using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Domain.ToDoAgg.Services
{
    public interface IToDoItemService
    {
        (T exception, bool isValid) CheckUser<T>(int userId) where T : Exception;
    }
}
