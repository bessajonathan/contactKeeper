using AutoMapper;
using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.User.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IViewModel<TokenViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public CreateUserCommandHandler(IContactKeeperContext context, IMapper mapper, ITokenService tokenService)
        {
            this.context = context;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<IViewModel<TokenViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = new Domain.Entities.User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = EncryptUtil.Encrypt(request.Password)
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return new ViewModel<TokenViewModel>
            {
                Data = tokenService.GenerateToken(user)
            };
        }
    }
}
