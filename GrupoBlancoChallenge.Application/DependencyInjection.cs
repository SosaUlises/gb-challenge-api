using GrupoBlancoChallenge.Application.Interfaces;
using GrupoBlancoChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GrupoBlancoChallenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
