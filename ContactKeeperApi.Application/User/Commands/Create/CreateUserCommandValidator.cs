using FluentValidation;

namespace ContactKeeperApi.Application.User.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(10).MinimumLength(6);
            RuleFor(x => x.PasswordConfirm).NotEmpty().NotNull().Equal(x => x.Password);
        }
    }
}
