using FluentValidation;

namespace ContactKeeperApi.Application.Contact.Queries.GetContact
{
    public class GetContactQueryValidator : AbstractValidator<GetContactQuery>
    {
        public GetContactQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
