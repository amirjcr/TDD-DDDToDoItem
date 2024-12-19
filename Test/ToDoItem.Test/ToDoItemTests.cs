using Microsoft.EntityFrameworkCore;
using ToDoItem.Application.Contexts;
using ToDoItem.Application.ToDoItems.Service;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.ToDoAgg;
using ToDoItem.Domain.ToDoAgg.Enums;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test;


[TestClass]
public class ToDoItemTests
{
    private IToDoItemService _todoItemService = default!;

    [TestInitialize]
    public void InitiateTestPreRequierties()
    {
        _todoItemService = new ToDoItemService();
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_TitleIsEmptyOrNull_ThrowException()
    {
        ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem(string.Empty, DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_UserCreatedIsDefaultOrZero_ThrownException()
    {
        ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, default, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void CreateItem_FinishedDateViolateGreaterThanCurrentDateRule_ThrowEexception()
    {
        ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("Do Job", DateTime.Now, Priority.Normal, 3, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainEntityNotFoundException))]
    public void CreateItem_CreatedUserIsNotFound_ThrownException()
    {
        ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 1, _todoItemService);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException), "User Can Not Create DoItem")]
    public void CreateItem_UserCreateDoItem_ThrowRuleValidationException()
    {
        ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 2, _todoItemService);
    }


    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_TitleIsNullOrEmpty_ThrownRuleValidationException()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        todoItem.UpdateToDoItem(string.Empty, todoItem.FinishedDate, todoItem.Priority);
    }


    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_FinishedDateIsDefault_ThrownExceptionN()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, default, todoItem.Priority);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_PriorityIsDefault_ThrownExceptionN()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, todoItem.FinishedDate, default);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainRulesViolatedException))]
    public void UpdateItem_FinishedDateIsSamllerThanCreateDate_ThrownExceptionN()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, DateTime.Now.AddDays(-2), todoItem.Priority);
    }

    [TestMethod]
    public void UpdateItem_PriorityIsHight_AcceptChanges()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        todoItem.UpdateToDoItem(todoItem.Title!, todoItem.FinishedDate, Priority.High);

        Assert.IsTrue(todoItem.Priority == Priority.High);
    }


    [TestMethod]
    public void UpdateItem_TitleChanged_AcceptChanges()
    {
        var todoItem = ToDoItem.Domain.ToDoAgg.ToDoItem.CreateItem("TodoItem", DateTime.Now.AddDays(3), Priority.Normal, 4, _todoItemService);
        var oldTitle = todoItem.Title;
        todoItem.UpdateToDoItem("new Title Is Assighned", todoItem.FinishedDate, todoItem.Priority);

        Assert.IsTrue(todoItem.Title != oldTitle);
    }
}