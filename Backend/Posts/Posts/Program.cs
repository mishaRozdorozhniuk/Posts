using Microsoft.Extensions.Configuration;
using Posts.DAL;
using Posts.Options;

var builder = WebApplication.CreateBuilder(args);
// Запускает сервер

builder.Services.AddControllers();
// Добавляем возможность добавлять контроллеры
builder.Services.AddEndpointsApiExplorer();
// Собирает все ендпоинты
builder.Services.AddSwaggerGen();
// Генерирует доку
builder.Services.AddDatabase(builder.Configuration);
// Генерирует доку

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
// сопоставление JWTOptions с секцией в джейсоне

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// Регестрируем миделвары
