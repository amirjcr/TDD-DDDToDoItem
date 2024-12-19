using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoItem.Persistance.Configuration
{
    public sealed class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem.Domain.ToDoAgg.ToDoItem>
    {
        public void Configure(EntityTypeBuilder<Domain.ToDoAgg.ToDoItem> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(400);
            builder.Property(p => p.FinishedDate).IsRequired();
            builder.Property(p => p.Priority).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(m => m.ToDoItems)
                .HasForeignKey(f => f.UserCreated)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
