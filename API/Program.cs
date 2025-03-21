using Infrastructure.Database;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WorkstationInfo;
using WorkstationInfo.Database;
using WorkstationInfo.Features.GetWorkstationInfo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetWorkstationInfoHandler).Assembly));// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 🔹 Yetkilendirme ve kimlik doğrulama servislerini ekle
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(); // ✅ Eksik servis eklendi!

// 🔹 Veri tabanı bağlantıları
builder.Services.AddDbContext<MesAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<WorkstationInfoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Shared Infrastructure ve WorkstationInfoModule yükle
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddWorkstationInfoModule(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();  // ✅ Kimlik doğrulama middleware eklendi
app.UseAuthorization();   // ✅ Yetkilendirme middleware eklendi

app.MapControllers();

app.Run();
