using Catalog.API.Middlewares;
using Catalog.API.Request.Product;
using Catalog.API.Validators;
using FluentValidation;
using Shared.Domain.Dtos;

namespace Catalog.API;

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
        services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
        services.AddScoped<IValidator<PaginationRequest>, PaginationRequestValidator>();

        return services;
    }
}
