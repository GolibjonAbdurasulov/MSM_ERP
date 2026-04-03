using WebAPI.Auth;
using WebAPI.Hubs;
using WebAPI.Interfaces;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS xizmatini qo'shish (Services qismiga)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Har qanday saytdan kelgan so'rovga ruxsat
            .AllowAnyMethod()   // POST, GET, PUT va hokazolarga ruxsat
            .AllowAnyHeader();  // Barcha headerlarga ruxsat
    });
});

// 1. CORS xizmatini to'g'ri sozlash
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:8090", "http://localhost:5173") // Front-end portlaringizni yozing
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // SignalR ulanishi uchun bu SHART
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IServicesService, ServicesService>();

var app = builder.Build();

// 2. CORS ni ishga tushirish (App qismiga, MapControllers dan oldin bo'lishi shart)
app.UseCors("AllowAll");


    app.UseSwagger();
    app.UseSwaggerUI();


app.MapHub<MonitoringHub>("/monitoringHub");


app.MapControllers();

app.Run();