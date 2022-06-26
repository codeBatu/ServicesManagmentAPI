using Business.Abstract;
using Business.Concrete;
using Model;
using Repository;
using Repository.DbContexts;
using Repository.RepositoryInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSingleton<SmartPulseServiceManagerContext>()
    .AddScoped<IServiceManagerRepository, ServiceManagerRepository>()
    .AddScoped<ILogRepository, LogRepository>()
    .AddScoped<IServiceSupply, ServiceManager>().AddScoped<ILogSupply, LogManager>();

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
