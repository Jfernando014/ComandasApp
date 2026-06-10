using ComandasApp.Application.Interfaces.Repositories;
using ComandasApp.Infrastructure.Persistence;
using ComandasApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComandasApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ComandasDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ComandasDbContext).Assembly.FullName)));

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
