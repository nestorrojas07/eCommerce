using Catalog.API;
using Catalog.API.Apis;
using Catalog.API.Extensions;
using Catalog.API.Middlewares;
using Catalog.Infraestructure;
using Catalog.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var withApiVersioning = builder.Services.AddApiVersioning();
//builder.AddDefaultOpenApi(withApiVersioning);

builder.Services.AddPresentation()
    .AddInfraestructure(builder.Configuration)
    .AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseMiddleware<GloblalExceptionHandlingMiddleware>();

app.MapCatalogApiV1();

app.Run();

