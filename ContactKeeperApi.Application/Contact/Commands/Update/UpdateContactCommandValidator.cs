using FluentValidation;

namespace ContactKeeperApi.Application.Contact.Commands.Update
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Number).NotEmpty().NotNull().MaximumLength(20);
        }
    }
}
