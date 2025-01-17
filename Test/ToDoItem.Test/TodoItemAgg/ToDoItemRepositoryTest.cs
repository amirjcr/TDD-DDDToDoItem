using Microsoft.EntityFrameworkCore;
using System;
using ToDoItem.Application.ToDoItems.Repository;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Application.UserAgg.Service;
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
    public class ToDoItemRepositoryTest
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
        public void Get_ItemWithZeroId_WillReturnNull()
        {
            Assert.IsNull(_repo.GetById(4839));
        }

        [TestMethod]
        public void Isert_ItemWithCorrectData_WillReturnOne()
        {
            var user = User.Create("user one test one ", new EmailAddress("tesdsfjkajflj4dsafafafe233t@gmail.com"), new Password("test password"), _userService);

            _userRepo.Insert(user);
            _userRepo.SaveChanges();

            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, user.Id, _service);

            _repo.Insert(item);
            Assert.IsTrue(_repo.SaveChanges() > 0);
        }

        [TestMethod]
        public void Search_ItemById_WillReturnItem()
        {
            var user = User.Create("user one user one test one ", new EmailAddress("testkjf49393akjdfkj@gmail.com"), new Password("test password"), _userService);

            _userRepo.Insert(user);
            _userRepo.SaveChanges();

            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, user.Id, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            Assert.IsNotNull(_repo.GetById(item.Id));
            Assert.AreEqual(item, _repo.GetById(item.Id));
        }

        [TestMethod]
        public void UpdateItem_ChangeName_AcceptChanges()
        {
            var user = User.Create("user one test one ", new EmailAddress("test@gmail.com"), new Password("test password"), _userService);

            _userRepo.Insert(user);
            _userRepo.SaveChanges();

            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, user.Id, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            var oldTitle = item.Title;

            item.Update("Make new TodoItem", item.FinishedDate, item.Priority);

            _repo.Update(item);
            _repo.SaveChanges();

            Assert.AreNotEqual(oldTitle, item.Title);
        }

        [TestMethod]
        public void DeleteItem_ByEntity_AcceptChanges()
        {
            var user = User.Create("user test one ", new EmailAddress("tesdf4334343sa2243daft@gmail.com"), new Password("test password"), _userService);

            _userRepo.Insert(user);
            _userRepo.SaveChanges();

            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, user.Id, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            var oldTitle = item.Title;

            _repo.Delete(item);
            _repo.SaveChanges();

            Assert.IsNull(_repo.GetById(item.Id));
        }

    }
}
