using infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MDBContext>();
    await context.Database.MigrateAsync();
    await SeedDatabase.SeedSuppliers(context);
    await SeedDatabase.SeedProducts(context);
    await SeedDatabase.SeedProductSuppliers(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
