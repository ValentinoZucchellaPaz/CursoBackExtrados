using APITorneo.Middleware;
using Configuration;
using Data_Access.DAOCartas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------CONFIG----------

//  AUTH

// CORS

// DI - DAOS
builder.Services.AddSingleton<IDAOCartas>(new DAOCartas(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI - SERVICES

// OPTIONS
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));


// ---------CONFIG----------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors();
//app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>(); //custom middleware

app.MapControllers();

app.Run();
