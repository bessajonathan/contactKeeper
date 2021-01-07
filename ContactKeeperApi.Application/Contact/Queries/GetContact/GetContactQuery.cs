using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Infrastructure;
using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Queries.GetContact
{
    public class GetContactQuery : Base, IRequest<IViewModel<ContactViewModel>>
    {
        public int Id { get; set; }
    }
}
