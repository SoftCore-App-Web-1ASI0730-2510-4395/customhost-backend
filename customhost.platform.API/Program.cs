
using customhost_backend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add CORS Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


if(connectionString== null) throw new InvalidOperationException("Connection string not found.");



builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

builder.Services.AddSwaggerGen(options=> { options.EnableAnnotations(); });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    // En desarrollo, recrear la base de datos si hay cambios en el modelo
    if (app.Environment.IsDevelopment())
    {
        context.Database.EnsureDeleted(); // Elimina la base de datos
        context.Database.EnsureCreated(); // La recreea con las nuevas tablas
    }
    else
    {
        context.Database.EnsureCreated(); // Solo crear si no existe en producción
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();