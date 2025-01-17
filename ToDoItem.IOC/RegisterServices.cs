using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoItem.IOC.Exceptions;
using ToDoItem.Persistance;

namespace ToDoItem.IOC
{
    public static class RegisterServices
    {
        public static IServiceCollection REGISTER_APPLICATION_SERVICES(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterPersistanceServices(configuration.GetConnectionString("DefaultConnection") ??
                throw new RegistererException(typeof(PersistanceDI), "connection string is null or not valid "));
            return services;
        }
    }
}
