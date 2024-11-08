using LowCostAvioFlights.Data;
using LowCostAvioFlights.Infrastructure;
using LowCostAvioFlights.Mappings;
using LowCostAvioFlights.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AmadeusApiSettings>(builder.Configuration.GetSection("AmadeusApiSettings"));

builder.Services.AddDbContext<LowCostAvioFlightsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightSearchDB")));

builder.Services.AddTransient<ILowCostAvioFlightsDbContext, LowCostAvioFlightsDbContext>();
builder.Services.AddTransient<IFlightSearchParametersRepository, FlightSearchParametersRepository>();
builder.Services.AddAutoMapper(typeof(FlightSearchParametersMappingProfile));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddTransient<AmadeusOAuthClient>();


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
