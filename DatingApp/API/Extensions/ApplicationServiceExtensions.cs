using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle -- Remove those line of code if we don't use Swagger
        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        services.AddCors();
        //3 options: AddTransient: very short lived service => The token service will be creted and disposed of within the request as soon as it's used and finished
        //AddScoped: After a request hit the endpoint in a controller, the framework instantiates a new instance of that controller
        //The controller looks at its dependencies or the framework does to create new instances of these services when the controllers created
        //When the controllers disposed of at the end of the HTTP request, then any dependent services are also disposed.
        //AddSingleton: The application first starts and is never disposed until the application has closed down. The service will hange around in memory.
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        return services;
    }
}