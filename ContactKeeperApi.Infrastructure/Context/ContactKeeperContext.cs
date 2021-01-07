using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactKeeperApi.Infra.Context
{
    public class ContactKeeperContext : DbContext, IContactKeeperContext
    {
        public ContactKeeperContext(DbContextOptions<ContactKeeperContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactKeeperContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSaving()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if(entry.Entity is Entity track)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            track.UpdatedAt = DateTime.Now;
                            break;
                        case EntityState.Added:
                            track.CreatedAt = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
