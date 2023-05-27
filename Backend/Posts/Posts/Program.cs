using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Posts.DAL;
using Posts.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Запускает сервер

builder.Services.AddControllers();
// Добавляем возможность добавлять контроллеры
builder.Services.AddEndpointsApiExplorer();
// Собирает все ендпоинты
builder.Services.AddSwaggerGen();
// Генерирует доку
builder.Services.AddDatabase(builder.Configuration);

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
// сопоставление JWTOptions с секцией в джейсоне

builder.Services
           .AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(jwt =>
           {
               var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fa5DRdkVwZeQnrDAcBrHCYwAWd6y2crPUbSZq4zUWBRFwDfKDXQWH38vZRfv"));

               jwt.SaveToken = true;

               jwt.TokenValidationParameters = new TokenValidationParameters()
               {
                   IssuerSigningKey = issuerSigningKey,
                   ValidIssuer = "http://localhost:5000",
                   ValidAudience = "http://localhost:8081",
                   ValidateIssuer = true,
                   ValidateAudience = true
               };
           });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Регестрируем миделвары