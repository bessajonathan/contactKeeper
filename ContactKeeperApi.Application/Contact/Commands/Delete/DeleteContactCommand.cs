using ContactKeeperApi.Application.Infrastructure;
using MediatR;

namespace ContactKeeperApi.Application.Contact.Commands.Delete
{
    public class DeleteContactCommand : CommandBase ,IRequest<Unit>
    {
        public DeleteContactCommand(int id,int userId)
        {
            Id = id;
            UserId = userId;
        }
        /// <summary>
        /// Id do contato a ser removido
        /// </summary>
        public int Id { get; private set; }
    }
}
