using DeviceManagement.API.Data;
using DeviceManagement.API.Repositories;
using DeviceManagement.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Device Management API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Management API v1"));
}

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");
app.UseAuthorization();
app.MapControllers();

app.Run();
