using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Context;
using ProjektAPBD.Repository;
using ProjektAPBD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAgreementService, AgreementService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IAgreementsRepository, AgreementsRepository>();
builder.Services.AddScoped<IDiscountsRepository, DiscountsRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwaresRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

