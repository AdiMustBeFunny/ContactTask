using Contacto.Domain.Abstractions;
using Contacto.Domain.Abstractions.EntityService;
using Contacto.DomainService.EntityService;
using Microsoft.Extensions.DependencyInjection;

namespace Contacto.DomainService
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IContactEntityService, ContactEntityService>();

            return services;
        }
    }
}
