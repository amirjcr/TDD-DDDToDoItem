using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.UserAgg.Service;
using ToDoItem.Domain.UserAgg.ValueObjects;
using ToDoItem.Sahred.Configurations;

namespace ToDoItem.Domain.UserAgg;

[Audtiable]
public sealed class User
{
    private readonly List<ToDoItem.Domain.ToDoAgg.ToDoItem> _todoItems = new List<ToDoItem.Domain.ToDoAgg.ToDoItem>();
    private User(string name, EmailAddress email, Password password, IUserService service)
    {
        CreateGaurd(name, email, password, service);
        Name = name;
        Email = email;
        Password = password;
    }

    private User() { }

    public int Id { get; private set; }
    public string? Name { get; private set; }
    public EmailAddress? Email { get; private set; }
    public Password? Password { get; private set; }
    public bool IsRestricted { get; private set; }
    public IReadOnlyCollection<ToDoItem.Domain.ToDoAgg.ToDoItem> ToDoItems => _todoItems;

    public void Update(EmailAddress email, IUserService service)
    {
        if (email is null)
            throw new DomainRulesViolatedException("you can not pass null email");
        else if (service.CheckEmailIsDuuplicate(email.Address!))
            throw new DomainRulesViolatedException("Email Can Not Duplicate");

        this.Email = email;
    }

    public void RestrictUser()
    {
        this.IsRestricted = true;
    }
    public static User Create(string name, EmailAddress email, Password password, IUserService service)
    {
        return new User(name, email, password, service);
    }


    private void CreateGaurd(string name, EmailAddress email, Password password, IUserService service)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainRulesViolatedException("Name Can Not be null or empty");
        else if (name.Length < 10 || name.Length > 150)
            throw new DomainRulesViolatedException("name lenght can not be less than 10 or more than 150 charctures");
        else if (email is null)
            throw new DomainRulesViolatedException("email Can not be empty");
        else if (service.CheckEmailIsDuuplicate(email.Address!))
            throw new DomainRulesViolatedException("Email Can Not Duplicate");
        else if (password is null)
            throw new DomainRulesViolatedException("password can not be empty");
    }
}


