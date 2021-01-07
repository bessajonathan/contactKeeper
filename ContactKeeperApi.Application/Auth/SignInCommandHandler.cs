using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common;
using ContactKeeperApi.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Auth
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, IViewModel<TokenViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly ITokenService tokenService;

        public SignInCommandHandler(IContactKeeperContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }
        public async Task<IViewModel<TokenViewModel>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(request.UserName.ToLower()));

            if (user is null)
                throw new BusinessException("Credenciais Incorretas");

            if (!EncryptUtil.Validate(request.Password, user.Password))
                throw new BusinessException("Credenciais Incorretas");

            var token = tokenService.GenerateToken(user);

            return new ViewModel<TokenViewModel>
            {
                Data = token
            };
        }
    }
}
