using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.ToDoItems.Repository;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Application.UserAgg.Service;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.ToDoAgg.Enums;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Domain.UserAgg.Service;
using ToDoItem.Domain.UserAgg.ValueObjects;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.TodoItemAgg
{
    [TestClass]
    public class ToDoItemChangeStateTests
    {
        private IUserRepository _userRepo = default!;
        private IToDoItemRepository _repo = default!;
        private IToDoItemService _service = default!;
        private IUserService _userService = default!;

        [TestInitialize]
        public void InitiatePreRequireties()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                            .UseInMemoryDatabase(databaseName: "ToDoItemInMemory")
                            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                            .Options;

            _repo = new ToDoItemRepository(new DatabaseContext(options));
            _userRepo = new UserRepository(new DatabaseContext(options));
            _service = new ToDoItemService(_repo, _userRepo);
            _userService = new UserService(_userRepo);
        }

        [TestMethod]
        public void UpdateItem_ChangeToDoItemStateToHight_AccptChanges()
        {
            var user = User.Create("user one test one ", new EmailAddress("tesdsfjkajflj4dsssafafafe233t@gmail.com"), new Password("test password"), _userService);

            _userRepo.Insert(user);
            _userRepo.SaveChanges();

            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, user.Id, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            item.Update(Priority.High);
            _repo.SaveChanges();
            Assert.IsTrue(item.Priority == Priority.High);
        }
    }
}
