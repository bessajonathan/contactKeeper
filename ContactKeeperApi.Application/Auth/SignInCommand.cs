using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using MediatR;

namespace ContactKeeperApi.Application.Auth
{
    public class SignInCommand : IRequest<IViewModel<TokenViewModel>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
