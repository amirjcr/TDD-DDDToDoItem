using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Domain.UserAgg.Service;

namespace ToDoItem.Application.UserAgg.Service
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool CheckEmailIsDuuplicate(string email)
        {
            return _repository.Any(c => c.Email!.Address == email);
        }
    }
}
