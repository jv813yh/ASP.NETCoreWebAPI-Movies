using Movies.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movies.Data.Interfaces;
using Movies.Data.Repositories;
using Movies.Api;
using Movies.Api.Interfaces;
using Movies.Api.Managers;


// WebApplication creates instantiates,
// which is used to configure and run a web application within the ASP.NET Core platform.
var builder = WebApplication.CreateBuilder(args);

// Connection string to the database is stored in the appsettings.json file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add the DbContext to the services collection
// The AddDbContext method is used to register the MoviesDbContext with the dependency injection container
// with lazy loading proxies enabled and ignoring the DetachedLazyLoadingWarning
builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options
    .UseSqlServer(connectionString)
    .UseLazyLoadingProxies()
    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
});

// Add the controllers to the services collection
builder.Services.AddControllers();

// Add the repositories to the services collection
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Add the AutoMapper to the services collection
builder.Services.AddAutoMapper(typeof(AutoMapperConfigurationProfile));


// Add the managers to the services collection
builder.Services.AddScoped<IPersonManager, PersonManager>();



// Build the application
var app = builder.Build();

// Endpoint routing is a middleware that maps the incoming HTTP requests to the endpoints
app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
