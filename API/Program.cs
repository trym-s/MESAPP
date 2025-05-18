using API.Behaviors;
using API.Middlewares;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OperatorPanel;
using OperatorPanel.Database;
using WorkstationInfo;
using WorkstationInfo.Database;
using WorkstationInfo.Features.Queries.GetWorkstationDetails;
using MQTTStreaming.Database;
using MQTTStreaming.DependencyInjection;

// ✅ ÖZEL YAPILANDIRMA
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development // istersen değiştir: Production, Staging...
});

// ✅ MERKEZİ appsettings.Shared.json yükleniyor
builder.Configuration
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), ".."))
    .AddJsonFile("Configuration/appsettings.Shared.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables();

// ✅ MediatR Handler'ları
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetWorkstationDetailsHandler).Assembly));

// ✅ Controller ve Endpoint
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Auth
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// 🔹 Database Connections
builder.Services.AddDbContext<MesAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<WorkstationInfoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<OperatorPanelDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ MQTT StreamingDbContext
builder.Services.AddDbContext<MqttStreamingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Validators
builder.Services.AddValidatorsFromAssemblyContaining<ChangeScodeCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 🔹 Modules
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddWorkstationInfoModule(builder.Configuration);
builder.Services.AddOperatorPanelModule(builder.Configuration);
builder.Services.AddMQTTStreamingModule(builder.Configuration); 



var app = builder.Build();

// 🔹 Middleware
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
