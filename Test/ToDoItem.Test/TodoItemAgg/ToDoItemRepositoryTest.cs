using Microsoft.EntityFrameworkCore;
using ToDoItem.Application.ToDoItems.Repository;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Domain.ToDoAgg.Enums;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.TodoItemAgg
{
    [TestClass]
    public class ToDoItemRepositoryTest
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

        [TestMethod]
        public void Get_ItemWithZeroId_WillReturnNull()
        {
            Assert.IsNull(_repo.GetById(4));
        }

        [TestMethod]
        public void Isert_ItemWithCorrectData_WillReturnOne()
        {
            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, 4, _service);
            _repo.Insert(item);

            Assert.IsTrue(_repo.SaveChanges() > 0);
        }

        [TestMethod]
        public void Search_ItemById_WillReturnItem()
        {
            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, 4, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            Assert.IsNotNull(_repo.GetById(item.Id));
            Assert.AreEqual(item, _repo.GetById(item.Id));
        }

        [TestMethod]
        public void UpdateItem_ChangeName_AcceptChanges()
        {
            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, 4, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            var oldTitle = item.Title;

            item.UpdateToDoItem("Make new TodoItem", item.FinishedDate, item.Priority);

            _repo.Update(item);
            _repo.SaveChanges();

            Assert.AreNotEqual(oldTitle, item.Title);
        }

        [TestMethod]
        public void DeleteItem_ByEntity_AcceptChanges()
        {
            var item = Domain.ToDoAgg.ToDoItem.CreateItem("test item", DateTime.Now.AddDays(3), Priority.Normal, 4, _service);
            _repo.Insert(item);
            _repo.SaveChanges();

            var oldTitle = item.Title;

            _repo.Delete(item);
            _repo.SaveChanges();

            Assert.IsNull(_repo.GetById(item.Id));
        }

    }
}
