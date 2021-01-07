using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Contact.Commands.Update
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
    {
        private readonly IContactKeeperContext context;

        public UpdateContactCommandHandler(IContactKeeperContext context)
        {
            this.context = context;
        }
        public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(contact is null)
                throw new NotFoundException($"O contato com id {request.Id} não existe");

            contact.Number = request.Number;

            context.Contacts.Update(contact);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
