using ToDoItem.Application.Contexts;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Sahred.Repository;

namespace ToDoItem.Application.ToDoItems.Repository
{
    public sealed class ToDoItemRepository : BaseRepository<ToDoItem.Domain.ToDoAgg.ToDoItem, int, IDatabaseContext>, IToDoItemRepository
    {
        public ToDoItemRepository(IDatabaseContext context)
            : base(context)
        {
        }
    }
}
