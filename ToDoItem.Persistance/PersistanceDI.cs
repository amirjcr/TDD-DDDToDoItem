using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.Contexts;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Persistance
{
    public static class PersistanceDI
    {
        public static IServiceCollection RegisterPersistanceServices(this IServiceCollection services, string connection)
        {

            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            {
                options.UseSqlServer(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableDetailedErrors();
            });

            return services;
        }
    }
}
