using Business.Abstract;
using Business.Concrete;
using Model;
using Repository;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddDbContext<SmartPulseServiceManagerContext>();
services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
services.AddScoped<IServiceManagerRepository, ServiceManagerRepository>();
services.AddScoped<ILogRepository, LogRepository>();
services.AddScoped<IServiceSupply, ServiceManager>().AddScoped<ILogRepository, LogRepository>();
services.AddScoped<ILogSupply, LogManager>();
services.AddScoped<IMailRepository, MailRepository>();
services.AddScoped<IMailSupply, MailManager>();
services.AddScoped<IEmailService, EmailManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:4000");
