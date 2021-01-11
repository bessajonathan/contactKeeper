using ContactKeeperApi.Application.Contact.Queries.GetContact;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Commands.Contact.Create
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, IViewModel<ContactViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMediator mediator;

        public CreateContactCommandHandler(IContactKeeperContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }
        public async Task<IViewModel<ContactViewModel>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Domain.Entities.Contact
            {
                Number = request.Number,
                UserId = request.UserId,
                Type = request.Type
            };

            context.Contacts.Add(contact);
            await context.SaveChangesAsync(cancellationToken);

            return await mediator.Send(new GetContactQuery(contact.Id,request.UserId));
        }
    }
}
