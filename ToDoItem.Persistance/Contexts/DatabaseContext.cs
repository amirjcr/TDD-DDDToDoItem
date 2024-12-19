﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.Contexts;
using ToDoItem.Domain.ToDoAgg;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Persistance.Configuration;

namespace ToDoItem.Persistance.Contexts
{
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAudtiableProperties();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public DbSet<ToDoItem.Domain.ToDoAgg.ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            UpdateShadowProperties();
            return base.SaveChanges();
        }

        private void UpdateShadowProperties()
        {
            var trackedEntites = this.ChangeTracker.Entries();

            if (trackedEntites is not null && trackedEntites.Any())
            {
                foreach (var entity in trackedEntites)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Property("CreatedDate").CurrentValue = DateTime.Now;
                        entity.Property("IsDeleted").CurrentValue = false;
                    }
                    else if (entity.State == EntityState.Deleted)
                    {
                        entity.Property("ModifiedDate").CurrentValue = DateTime.Now;
                        entity.Property("IsDeleted").CurrentValue = true;
                    }
                    else
                    {
                        entity.Property("ModifiedDate").CurrentValue = DateTime.Now;
                    }
                }
            }
        }
    }
}
