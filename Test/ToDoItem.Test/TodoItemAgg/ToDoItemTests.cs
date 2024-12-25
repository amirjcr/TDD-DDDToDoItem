using Microsoft.EntityFrameworkCore;
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

namespace ToDoItem.Test.TodoItemAgg;


[TestClass]
public class ToDoItemTests
{
    private IToDoItemService _todoItemService = default!;
    private IUserRepository _userRepo = default!;
    private IToDoItemRepository _repo = default!;
    private IUserService _userService = default!;
    private User _user = default!;
    private DatabaseContext _context = default!;
    private readonly static Random random = new Random(493);
    private string[] perdifinedemailCharctures = new string[] { "s", "a", "e", "a", "8", "11", "14", "4", "9", "c", "e", "i", "i", "z", "t", "300", "148s" };

    [TestInitialize]
    public void InitiateTestPreRequierties()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
                        .UseInMemoryDatabase("TestDb")
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .Options;

        _context = new DatabaseContext(options);
        _userRepo = new UserRepository(_context);
        _repo = new ToDoItemRepository(_context);

        _todoItemService = new ToDoItemService(_repo, _userRepo);
        _userService = new UserService(_userRepo);

        string email = string.Empty;
        for (int i = 0; i < random.Next(8, 15); i++)
            email += perdifinedemailCharctures[random.Next(0, perdifinedemailCharctures.Length)];

        _user = User.Create("test user14384 test againg", new EmailAddress($"{email}@gmail.com"), new Password("test password 1483"), _userService);
        _userRepo.Insert(_user);
        _userRepo.SaveChanges();
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_TitleIsEmptyOrNull_ThrowException()
    {
        Domain.ToDoAgg.ToDoItem.CreateItem(string.Empty, DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_UserCreatedIsDefaultOrZero_ThrownException()
    {
        Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, default, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_FinishedDateViolateGreaterThanCurrentDateRule_ThrowEexception()
    {
        Domain.ToDoAgg.ToDoItem.CreateItem("Do Job", DateTime.Now, Priority.Normal, 3, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainEntityNotFoundException))]
    public void CreateItem_CreatedUserIsNotFound_ThrownException()
    {
        Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 1044, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException), "User Can Not Create DoItem")]
    public void CreateItem_UserCreateDoItem_ThrowRuleValidationException()
    {
        _user.RestrictUser();
        _userRepo.SaveChanges();
        Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
    }


    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_TitleIsNullOrEmpty_ThrownRuleValidationException()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        todoItem.UpdateToDoItem(string.Empty, todoItem.FinishedDate, todoItem.Priority);
    }


    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_FinishedDateIsDefault_ThrownExceptionN()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, default, todoItem.Priority);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_PriorityIsDefault_ThrownExceptionN()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, todoItem.FinishedDate, default);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_FinishedDateIsSamllerThanCreateDate_ThrownExceptionN()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, DateTime.Now.AddDays(-2), todoItem.Priority);
    }

    [TestMethod]
    public void UpdateItem_PriorityIsHight_AcceptChanges()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, todoItem.FinishedDate, Priority.High);

        Assert.IsTrue(todoItem.Priority == Priority.High);
    }


    [TestMethod]
    public void UpdateItem_TitleChanged_AcceptChanges()
    {
        var todoItem = Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, _user.Id, _todoItemService);
        var oldTitle = todoItem.Title;
        todoItem.UpdateToDoItem("new Title Is Assighned", todoItem.FinishedDate, todoItem.Priority);

        Assert.IsTrue(todoItem.Title != oldTitle);
    }
}