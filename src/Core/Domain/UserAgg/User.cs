using ToDoItem.Sahred.Configurations;

namespace ToDoItem.Domain.UserAgg;

[Audtiable]
public sealed class User
{
    private readonly List<ToDoItem.Domain.ToDoAgg.ToDoItem> _todoItems = new List<ToDoItem.Domain.ToDoAgg.ToDoItem>();
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    private User() { }

    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }

    public IReadOnlyCollection<ToDoItem.Domain.ToDoAgg.ToDoItem> ToDoItems => _todoItems;
}


