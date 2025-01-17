using FluentValidation;
using Microsoft.Extensions.Logging;
using ToDoItem.Application.Messages;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Application.Utilities;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Domain.UserAgg.Service;
using ToDoItem.Sahred.Dtos;
using ToDoItem.Sahred.Utilites;

namespace ToDoItem.Application.UserAgg.Commands.Create
{
    public sealed class UserCreateCommandHanlder : ApplicationDefaultCommandHanlder<UserCreateCommand, int>
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserService _userService;

        public UserCreateCommandHanlder(ILogger<UserCreateCommandHanlder> logger, IValidator<UserCreateCommand> validator,
                                        IUserRepository userRepo, IUserService userService)
            : base(logger, validator)
        {
            _userRepo = userRepo;
            _userService = userService;
        }

        protected override async Task<ResponseDto<int>> ExecutAsync(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var resposne = new ResponseDto<int>();
            var user = User.Create(request.Name, request.Email, request.Password, _userService);
            _userRepo.Insert(user);
            var savingResult = await _userRepo.SaveChangesAsync(cancellationToken);

            return savingResult > 0 ? resposne.OnSuccess(ApplicationMessages.SAVING_SUCCESSFUL, user.Id)
                                  : resposne.OnFailer(ApplicationMessages.SAVING_FAILED, 0);
        }
    }
}
