
namespace ToDoItem.IOC.Exceptions;

public class RegistererException : Exception
{
    public RegistererException(Type registeredType, string message)
        : base($"Can not Register {registeredType} bacuase : {message}")
    { }
}
