using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Sahred.Configurations;

namespace ToDoItem.Persistance.Configuration
{
    public static class AudtiableConfiguration
    {
        public static void AddAudtiableProperties(this ModelBuilder builder)
        {
            var entities = builder.Model.GetEntityTypes();

            foreach (var entityType in entities)
            {
                var audtiableAttribute = entityType.ClrType.GetCustomAttribute(typeof(Audtiable));

                if (audtiableAttribute is not null)
                {
                    builder.Entity(entityType.ClrType).Property<DateTime>("ModifiedDate");
                    builder.Entity(entityType.ClrType).Property<bool>("IsDeleted").IsRequired();
                }
            }
        }
    }
}
