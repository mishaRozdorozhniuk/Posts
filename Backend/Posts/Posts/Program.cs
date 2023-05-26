var builder = WebApplication.CreateBuilder(args);
// Запускает сервер

builder.Services.AddControllers();
// Добавляем возможность добавлять контроллеры
builder.Services.AddEndpointsApiExplorer();
// Собирает все ендпоинты
builder.Services.AddSwaggerGen();
// Генерирует доку

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
