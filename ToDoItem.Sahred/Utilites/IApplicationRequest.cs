
using MediatR;

namespace ToDoItem.Sahred.Utilites
{
    public interface IApplicationRequest : IRequest
    {
    }

    public interface IApplicationRequest<T> : IRequest<T>
    { }


    public interface IApplicationRequestHandler<T> : IRequestHandler<T>
        where T : IApplicationRequest
    { }

    public interface IApplicationRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IApplicationRequest<TResponse>
    { }
}
