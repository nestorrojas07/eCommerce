using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Contracts.Repositories;
using Order.Infraestructure.Repositories;
using Order.Infraestructure.Constants;
using Microsoft.Extensions.Http;

namespace Order.Infraestructure;

public static class DependencyInyection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.Mariachi(configuration);
        services.AddRepositories();
        

        return services;

    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("CatalogDb"));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        return services;
    }

    public static IServiceCollection Mariachi(this IServiceCollection services, IConfiguration configuration)
    {
        string productBaseUrl = configuration.GetValue<string>("ProductBaseUrl") 
            ?? throw new ArgumentNullException("ProductBaseUrl Is not aviable");

        services.AddScoped<IProductRepository>(sp => new ProductRepository(new HttpClient() { 
            BaseAddress = new Uri(productBaseUrl)
        }));

        return services;
    }

}
