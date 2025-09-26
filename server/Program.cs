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

// Geliştirme ortamında Swagger aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "YKS-TAYFA API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Controller’ları bağla
app.MapControllers();

app.Run();
