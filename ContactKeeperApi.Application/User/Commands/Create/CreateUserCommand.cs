using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.User.Commands.Create
{
    public class CreateUserCommand : IRequest<IViewModel<TokenViewModel>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
