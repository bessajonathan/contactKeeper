using FluentValidation;

namespace ContactKeeperApi.Application.Auth
{
    public class SignInCommandValidator :AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
