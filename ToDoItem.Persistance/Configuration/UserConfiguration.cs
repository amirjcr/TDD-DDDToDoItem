using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoItem.Domain.UserAgg;

namespace ToDoItem.Persistance.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Email).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
        }
    }
}
