using System.Net.Http;
using YKSTAYFA.Helpers;

namespace YKSTAYFA.Services
{
    /// <summary>
    /// Tüm isteklerde kullanılacak ortak HttpClient.
    /// </summary>
    public class ApiClient
    {
        public HttpClient Http { get; }

        public ApiClient(HttpClient http)
        {
            Http = http;
            if (Http.BaseAddress == null)
            {
                Http.BaseAddress = new Uri(ApiConfig.BaseUrl);
            }
        }
    }
}
