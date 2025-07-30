using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zerphin.RentACar.Server.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
