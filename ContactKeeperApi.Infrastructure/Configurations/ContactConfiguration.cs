using ContactKeeperApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactKeeperApi.Infra.Configurations
{
    class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).HasMaxLength(20).IsRequired();
            builder.HasOne(x => x.User).WithMany(y => y.Contacts).HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
