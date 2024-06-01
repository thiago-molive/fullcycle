using EDA_FC3.Domain.Interfaces;
using EDA_FC3.Infra.Persistence.Balances;
using EDA_FC3.Initializers;
using EDA_FC3.Midlewares;
using MySql.Data.MySqlClient;
using System.Data;
using EDA_FC3.Infra.Persistence.BalanceAccount;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransitConfig(builder.Configuration);

builder.Services.AddScoped<IDbConnection>((sp) => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBalancesRepository, BalancesRepository>();
builder.Services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();

builder.Services.AddExceptionHandler<ExceptionMidleare>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseExceptionHandler();

app.Run();
