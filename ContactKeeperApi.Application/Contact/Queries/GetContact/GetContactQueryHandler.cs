using AutoMapper;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Contact.Queries.GetContact
{
    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, IViewModel<ContactViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMapper mapper;

        public GetContactQueryHandler(IContactKeeperContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IViewModel<ContactViewModel>> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var contact = await context
                .Contacts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (contact is null)
                throw new NotFoundException($"O contato com id {request.Id} não existe");

            return new ViewModel<ContactViewModel>
            {
                Data = mapper.Map<ContactViewModel>(contact)
            };
        }
    }
}
