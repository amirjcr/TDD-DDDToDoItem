using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Sahred.Repository;

namespace ToDoItem.Application.Contexts
{
    public interface IDatabaseContext : IDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<ToDoItem.Domain.ToDoAgg.ToDoItem> ToDoItems { get; set; }
    }
}
