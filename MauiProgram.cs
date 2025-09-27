using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using YKSTAYFA.Helpers;
using YKSTAYFA.Services;

namespace YKSTAYFA
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>();

            // HttpClient + BaseUrl
            builder.Services.AddHttpClient<ApiClient>(client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseUrl);
            });

            // Servislerini ekle (gerektikçe buraya yenilerini ekleyebilirsin)
            builder.Services.AddSingleton<TestService>();

            return builder.Build();
        }
    }
}
