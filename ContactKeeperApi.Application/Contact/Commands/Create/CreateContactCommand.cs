using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Infrastructure;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Domain.Enum;
using MediatR;

namespace ContactKeeperApi.Application.Commands.Contact.Create
{
    public class CreateContactCommand : CommandBase,IRequest<IViewModel<ContactViewModel>>
    {

        public string Number { get; set; }
        public EContactType Type { get; set; }
    }
}
