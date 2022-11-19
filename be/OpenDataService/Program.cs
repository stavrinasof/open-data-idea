using OpenDataService.Interfaces;
using OpenDataService.Models;
using OpenDataService.Repositories;
using OpenDataService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IReportRepository<MaintenaceReport>, MaintenanceReportsRepository>();
builder.Services.AddSingleton<IReportRepository<IncidentReport>, IncidentReportsRepository>();

builder.Services.AddSingleton<IAreaMappingService, MockAreaMappingService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IFireBaseNotificationService, FirebaseNotificationService>();
builder.Services.AddScoped<ISlackNotificationService, SlackNotificationService>();

var appSettingsSection = builder.Configuration.GetSection("FirebaseSettings");
builder.Services.Configure<FirebaseSettings>(appSettingsSection);

builder.Services.AddControllers();
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
