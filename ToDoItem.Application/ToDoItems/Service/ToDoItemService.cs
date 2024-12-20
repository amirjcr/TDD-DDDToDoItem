using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.Contexts;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.ToDoAgg.Repository;
using ToDoItem.Domain.ToDoAgg.Services;

namespace ToDoItem.Application.ToDoItems.Service
{
    public sealed class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository? _repository;

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


        private bool CheckUserIsExisits(int userId) => userId == 1 ? true : false;

        private bool CheckUserCanCreateToDoItem(int userId) => userId == 2 ? true : false;
    }
}
