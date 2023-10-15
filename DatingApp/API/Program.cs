using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle -- Remove those line of code if we don't use Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//3 options: AddTransient: very short lived service => The token service will be creted and disposed of within the request as soon as it's used and finished
//AddScoped: After a request hit the endpoint in a controller, the framework instantiates a new instance of that controller
//The controller looks at its dependencies or the framework does to create new instances of these services when the controllers created
//When the controllers disposed of at the end of the HTTP request, then any dependent services are also disposed.
//AddSingleton: The application first starts and is never disposed until the application has closed down. The service will hange around in memory.
builder.Services.AddScoped<ITokenService, TokenService>();

//Adding authentication service so that our server have infromation to take a look at the token
//and validate it based on the issuer signing key we have implemented.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option => {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            //Check the token signing key and make sure it is valid
            ValidateIssuerSigningKey = true,
            //Specify what our issuer signing key is
            IssuerSigningKey = new SymmetricSecurityKey(Encoding
            .UTF8.GetBytes(builder.Configuration["TokenKey"])),
            //We did not pass the Validate Issuer and Audience so we set them false
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline. 
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// if (app.Environment.IsDevelopment())-- Remove those line of code if we don't use Swagger
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

//Add authentication middleware before the map controllers
app.UseAuthentication(); //Ask if you have a valid token
app.UseAuthorization(); //Ok you have the token, so what are you allowed to do?

app.MapControllers();

app.Run();
