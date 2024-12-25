using Microsoft.EntityFrameworkCore;
using ToDoItem.Application.ToDoItems.Repository;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.TodoItemAgg
{
    [TestClass]
    public class ToDoItemServiceTest
    {
        private IUserRepository _userRepo = default!;
        private IToDoItemRepository _repo = default!;
        private IToDoItemService _service = default!;

        [TestInitialize]
        public void InitiatePreRequireties()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "ToDoItemInMemory")
                            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                            .Options;

            var context = new DatabaseContext(options);
            _userRepo = new UserRepository(context);
            _repo = new ToDoItemRepository(context);
            _service = new ToDoItemService(_repo, _userRepo);
        }

        //[TestMethod]
        ////[ExpectedException]
        //public void CreateItemm_CheckUserId_ExceptedError()
        //{

        //}
    }
}
