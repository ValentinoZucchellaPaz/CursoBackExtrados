using API.Middleware;
using Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.UserService;
using Services.AuthService;
using DAO_Entidades.DAO.DAOUser;
using DAO_Entidades.DAO.DAOBook;
using DAO_Entidades.DAO.DAORefreshToken;
using Services.BookService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI (https://aka.ms/aspnetcore/swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

// Configure cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});


// DI - Daos
builder.Services.AddSingleton<IDAOUser>(new DAOUser(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IDAOBook>(new DAOBook(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IDAORefreshToken>(new DAORefreshToken(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI - Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddHttpContextAccessor();

// Configure options
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>(); //custom middleware

app.MapControllers();

app.Run();
