using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.User.Commands.Create
{
    public class CreateUserCommand : IRequest<IViewModel<UserViewModel>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
