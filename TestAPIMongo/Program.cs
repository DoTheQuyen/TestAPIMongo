using FluentValidation;
using Serilog;
using TestAPIMongo.Data.DataAccess;
using TestAPIMongo.Data.Interface;
using TestAPIMongo.Data.Models;
using TestAPIMongo.Services.Interface;
using TestAPIMongo.Services.Services;
using TestAPIMongo.Validation;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // optional, if using appsettings.json
    .WriteTo.Console()   // logs to console
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // logs to file, new file every day
    .MinimumLevel.Information() // log info and above
    .CreateLogger();

builder.Host.UseSerilog(); // Replace default logger with Serilog

// Add services to the container.
builder.Services.Configure<DatabaseSetting>(
    builder.Configuration.GetSection("DatabaseSetting"));

//Inject services
builder.Services.AddScoped<IValidator<OrdersFilterModel>, OrdersFilterValidation>();
builder.Services.AddScoped<IOrders, Orders>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
