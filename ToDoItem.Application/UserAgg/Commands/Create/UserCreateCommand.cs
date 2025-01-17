
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
using ToDoItem.Domain.UserAgg.ValueObjects;
using ToDoItem.Sahred.Dtos;
using ToDoItem.Sahred.Utilites;

namespace ToDoItem.Application.UserAgg.Commands.Create
{
    public sealed record UserCreateCommand(string Name, EmailAddress Email, Password Password) : IApplicationRequest<ResponseDto<int>>;

    public sealed class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(100).WithMessage("Name can not be greater than 100 charctures")
                .MinimumLength(10).WithMessage("Name can not be less than 10 charctures");

            RuleFor(p => p.Email)
                .NotNull().WithMessage("email can not be null or empty");

            RuleFor(p => p.Password)
                .NotNull().WithMessage("password can not be null or empty");
        }
    }
}
