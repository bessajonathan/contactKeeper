using AutoMapper;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common;
using ContactKeeperApi.Common.Exceptions;
using ContactKeeperApi.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.User.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IViewModel<UserViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IContactKeeperContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IViewModel<UserViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirm)
                throw new BusinessException("Senhas não conferem");

            var user = new Domain.Entities.User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = EncryptUtil.Encrypt(request.Password)
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return new ViewModel<UserViewModel>
            {
                Data = mapper.Map<UserViewModel>(user)
            };
        }
    }
}
