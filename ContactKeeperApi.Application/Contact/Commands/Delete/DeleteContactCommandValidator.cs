using FluentValidation;

namespace ContactKeeperApi.Application.Contact.Commands.Delete
{
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
