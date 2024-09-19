using Cepdi.Application.Interfaces;
using Cepdi.Persistence.Context;
using Cepdi.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cepdi.Persistence.Extentions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationDbContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
            return services;
        }
    }
}
