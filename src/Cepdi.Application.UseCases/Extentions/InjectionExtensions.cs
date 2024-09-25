 using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cepdi.Application.UseCases.Extentions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
