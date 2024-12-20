using Microsoft.EntityFrameworkCore;
using ToDoItem.Application.ToDoItems.Repository;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.TodoItemAgg
{
    [TestClass]
    public class ToDoItemServiceTest
    {
        private IToDoItemRepository _repo = default!;
        private IToDoItemService _service = default!;

        [TestInitialize]
        public void InitiatePreRequireties()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "ToDoItemInMemory")
                            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                            .Options;

            _repo = new ToDoItemRepository(new DatabaseContext(options));
            _service = new ToDoItemService();
        }

        //[TestMethod]
        ////[ExpectedException]
        //public void CreateItemm_CheckUserId_ExceptedError()
        //{

        //}
    }
}
