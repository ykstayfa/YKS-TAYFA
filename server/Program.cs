using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Controller ve Endpoint desteği
builder.Services.AddControllers();

// Swagger (OpenAPI) ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YKS-TAYFA API",
        Version = "v1",
        Description = "YKS-TAYFA uygulaması için backend API"
    });
});

var app = builder.Build();

// Prod’da da Swagger açık kalsın
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YKS-TAYFA API v1");
});

// Container içinde HTTPS endpoint’i tanımlamadığımız için yönlendirmeyi kapatıyoruz
// app.UseHttpsRedirection();

app.UseAuthorization();

// Controller'ları bağla
app.MapControllers();

// Render'ın verdiği PORT'u dinle (yoksa 8080)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
