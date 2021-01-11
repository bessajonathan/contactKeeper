using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Infrastructure;
using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Queries.GetContact
{
    public class GetContactQuery : CommandBase, IRequest<IViewModel<ContactViewModel>>
    {
        public GetContactQuery(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
        public int Id { get; private set; }
    }
}
