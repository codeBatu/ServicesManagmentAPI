using Business.Abstract;
using Business.Concrete;
using Business.Helpers;
using Model;
using Repository;
using Repository.DbContexts;
using Repository.RepositoryInterface;
using System.Text.Json.Serialization;
using Business.Helpers.Authorization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

{
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

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddSwaggerGen();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IServiceManagerRepository, ServiceManagerRepository>();
    services.AddScoped<IServiceSupply, ServiceManager>();

    services.AddScoped<ILogRepository, LogRepository>();
    services.AddScoped<ILogSupply, LogManager>();

    services.AddScoped<IMailRepository, MailRepository>();
    services.AddScoped<IMailSupply, MailManager>();

    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IJwtUtils, JwtUtils>();

    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<IAccountSupply, AccountManager>();
    
    services.AddScoped<IRoleRepository, RoleRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Service Management API"));

    //app.UseHttpsRedirection();

    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    //app.UseAuthorization();

    app.MapControllers();
}

app.Run("http://localhost:4000");
