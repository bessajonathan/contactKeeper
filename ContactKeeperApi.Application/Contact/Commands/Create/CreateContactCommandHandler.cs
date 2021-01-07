using AutoMapper;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Commands.Contact.Create
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, IViewModel<ContactViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMapper mapper;

        public CreateContactCommandHandler(IContactKeeperContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IViewModel<ContactViewModel>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Domain.Entities.Contact
            {
                Number = request.Number,
                UserId = request.UserId
            };

            context.Contacts.Add(contact);
            await context.SaveChangesAsync(cancellationToken);

            contact.User = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            return new ViewModel<ContactViewModel>
            {
                Data = mapper.Map<ContactViewModel>(contact)
            };
        }
    }
}
