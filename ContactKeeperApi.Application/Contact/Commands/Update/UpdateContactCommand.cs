using ContactKeeperApi.Application.Infrastructure;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Commands.Update
{
    public class UpdateContactCommand : CommandBase, IRequest<Unit>
    {
        public int Id { get; set; }
        public string Number { get; set; }
    }
}
