using LowCostAvioFlights.Data;
using LowCostAvioFlights.Infrastructure;
using LowCostAvioFlights.Mappings;
using LowCostAvioFlights.Repositories;
using LowCostAvioFlights.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<AmadeusOAuthClient>();
builder.Services.AddTransient<FlightSearchService>();
builder.Services.AddTransient<AmadeusApiClientService>();
builder.Services.AddTransient<ITokenCacheService, TokenCacheService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath,true);
});

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
