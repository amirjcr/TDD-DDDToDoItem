using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoItem.Domain.UserAgg;

namespace ToDoItem.Persistance.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(p => p.Password).Property(p => p.Value).IsRequired().HasMaxLength(150);
            builder.OwnsOne(p => p.Email).Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
        }
    }
}
