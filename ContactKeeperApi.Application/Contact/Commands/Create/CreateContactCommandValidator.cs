using FluentValidation;

namespace ContactKeeperApi.Application.Commands.Contact.Create
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(x => x.Number).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
