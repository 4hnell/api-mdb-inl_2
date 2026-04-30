using api.Helpers;
using core.Interfaces;
using infrastructure.Data;
using infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(options =>
{
    options.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA3MDU2MDAwIiwiaWF0IjoiMTc3NTU2NDU1NyIsImFjY291bnRfaWQiOiIwMTlkNjdlNDNmNzI3ZjVjOWFhNzlhZTQ2ZWViY2FlZiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa25reWEzMXhubjVrd25xOGM5Z3kwNm55Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.Yli6Hm3sh_cCe3RJPVY7paXul4UDqypG2yQYeziOJ_k65bFwNmtSZCxhKYaZNrSLetwD-ExpyjgjvUG4P1j0gTq5_1LENE_TiQbZc6nAQRlegaTsQhEE1r7ieBmovkkwK0aATLfTH4d0sS52shyxZ434Ur1Pby78TPHlo0EcL6sXK-w0Bh8D3puvGrvAMzNoo-p8UorIRmDzk2JAhT90SMMHzHlA0dmNprVVIi4VrGCHK1bPyefK7mVKEjWblUwtHXqu9lqbYDJ0tp8FrBh4fQaiFHgZxiXDBDeFHNmhAoBACFDpu2ySrVHkk5JPoeOB3PxehjjvYrkqF_ceK-7WHw";
    options.AddProfile(new MappingProfiles());
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
