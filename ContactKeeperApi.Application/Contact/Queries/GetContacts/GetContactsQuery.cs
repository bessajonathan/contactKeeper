using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Infrastructure;
using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Queries.GetContacts
{
    public class GetContactsQuery : QueryBase , IRequest<IListViewModel<ContactViewModel>>
    {
    }
}
