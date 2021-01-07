using ContactKeeperApi.Application.Auth.ViewModel;

namespace ContactKeeperApi.Application.Interfaces
{
    public interface ITokenService
    {
        TokenViewModel GenerateToken(Domain.Entities.User user);
    }
}
