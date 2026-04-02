using WebAPI.Auth;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// AGAR siz localda ishlayotgan bo'lsangiz va HTTPS muammo bersa, 
// vaqtincha buni o'chirib turing yoki https:// emas http:// dan foydalaning
// app.UseHttpsRedirection(); 

app.MapControllers();

app.Run();