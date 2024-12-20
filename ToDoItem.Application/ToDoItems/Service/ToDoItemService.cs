using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.Contexts;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Domain.UserAgg.Repository;

namespace ToDoItem.Application.ToDoItems.Service
{
    public sealed class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository? _repository;
        private readonly IUserRepository? _userRepository;

        public ToDoItemService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ToDoItemService(IToDoItemRepository repository)
        {
            _repository = repository;
        }
        public ToDoItemService()
        {
        }

        public (T exception, bool isValid) CheckUser<T>(int userId) where T : Exception
        {
            Exception ex = default!;
            if (CheckUserIsExisits(userId))
                ex = new DomainEntityNotFoundException($"User With Id : {userId} Not Found");
            else if (CheckUserCanCreateToDoItem(userId))
                ex = new DomainRulesViolatedException("User Can Not Create DoItem");

            if (ex is not null)
                return ((T)ex, false);
            else
                return (default!, true);
        }

        private bool CheckUserIsExisits(int userId) => _repository!.Any(c => c.UserCreated == userId);

        private bool CheckUserCanCreateToDoItem(int userId) => _userRepository!.CanUserCreateItem(userId);
    }
}
