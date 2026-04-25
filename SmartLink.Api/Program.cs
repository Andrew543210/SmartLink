var builder = WebApplication.CreateBuilder(args);

// 1. Додаємо підтримку контролерів
builder.Services.AddControllers();

// Додаємо Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Додаємо політику CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

app.UseCors("AllowAll");
// 2. Налаштовуємо Swagger для зручної перевірки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Мапимо (підключаємо) контролери
app.MapControllers();

app.Run();
