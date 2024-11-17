using Catalog.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

        dbContext.Database.Migrate();
    }
}
