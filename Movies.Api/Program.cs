using Movies.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movies.Data.Interfaces;
using Movies.Data.Repositories;
using Movies.Api;
using Movies.Api.Interfaces;
using Movies.Api.Managers;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;


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

// 
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<MoviesDbContext>();


// Add the controllers to the services collection and configure the JSON serializer to use string enums
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add the OpenAPI/Swagger generator to the services collection
builder.Services.AddEndpointsApiExplorer();

// Add the OpenAPI/Swagger generator to the services collection
builder.Services.AddSwaggerGen(options =>
options.SwaggerDoc("movies", new OpenApiInfo 
    { 
        Version = "v1", 
        Title = "Simple movie API",
        Description = "Web API for movie database created by using ASP.NET technology",
    })
);

// Add the repositories to the services collection
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

// Add the AutoMapper to the services collection
builder.Services.AddAutoMapper(typeof(AutoMapperConfigurationProfile));

// Add the managers to the services collection
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IMovieManager, MovieManager>();
builder.Services.AddScoped<IGenreManager, GenreManager>();

// Build the application
var app = builder.Build();

// Endpoint routing is a middleware that maps the incoming HTTP requests to the endpoints
app.MapControllers();

// If the environment is development, use the Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("movies/swagger.json", "Movies API - v1");
    });
}

// Use the authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Welcome");


// 
using(var scope = app.Services.CreateScope())
{
    // Get the RoleManager service from the service provider
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create all roles in the application using the RoleManager service
    await CreateAllRoles(roleManager);
}   

// Run the application
app.Run();



// Create all roles in the application using the RoleManager service and the UserRoles class 
// for string constants representing the roles and create the roles if they do not exist
async Task CreateAllRoles(RoleManager<IdentityRole> roleManager)
{
    // Get all the constants from the UserRoles class
    FieldInfo[] constants = typeof(UserRoles)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(fieldInfo => fieldInfo.IsLiteral
            && !fieldInfo.IsInitOnly
            && fieldInfo.FieldType == typeof(string))
        .ToArray();

    // Get all the roles from the constants
    string[] roles = constants
        .Select(fieldInfo => fieldInfo.GetRawConstantValue())
        .OfType<string>()
        .ToArray();

    // Create the roles if they do not exist
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
