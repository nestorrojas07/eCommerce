using Order.API;
using Order.API.Extensions;
using Order.API.Middlewares;
using Order.Services;
using Order.Infraestructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
