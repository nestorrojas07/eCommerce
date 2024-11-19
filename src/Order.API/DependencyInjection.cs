using Order.API.Middlewares;
//using Order.API.Request.Product;
//using Order.API.Validators;
using FluentValidation;
using Order.API.Middlewares;
using Shared.Domain.Dtos;
using Order.API.Request;
using Order.API.Validators;

namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<GloblalExceptionHandlingMiddleware>();
        services.AddValidators();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();
        services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();
        services.AddScoped<IValidator<AddOrderDetailRequest>, AddOrderDetailRequestValidator>();
        services.AddScoped<IValidator<UpdateOrderDetailRequest>, UpdateOrderDetailRequestValidator>();

        return services;
    }
}
