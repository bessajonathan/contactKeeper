using ContactKeeperApi.Application.Infrastructure;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Commands.Update
{
    public class UpdateContactCommand : Base, IRequest<Unit>
    {
        public int Id { get; set; }
        public string Number { get; set; }
    }
}
