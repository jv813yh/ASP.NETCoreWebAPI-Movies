using Movies.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movies.Data.Interfaces;
using Movies.Data.Repositories;


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

// Add the repositories to the services collection
builder.Services.AddScoped<IPersonRepository, PersonRepository>();


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
