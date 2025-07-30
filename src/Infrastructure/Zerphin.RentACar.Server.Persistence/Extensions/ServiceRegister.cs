using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zerphin.RentACar.Server.Domain.Options;
using Zerphin.RentACar.Server.Persistence.Context;


namespace Zerphin.RentACar.Server.Persistence.Extensions
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringOptions = new ConnectionStringOption();
            configuration.GetSection(ConnectionStringOption.Key).Bind(connectionStringOptions);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStringOptions.SqlServer, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });
            });

            return services;
        }
    }
}
