using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Application.Interfaces
{
    public interface IContactKeeperContext
    {
        DbSet<Domain.Entities.User> Users { get; set; }
        DbSet<Domain.Entities.Contact> Contacts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
