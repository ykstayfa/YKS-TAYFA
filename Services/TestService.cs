using System.Threading.Tasks;

namespace YKSTAYFA.Services
{
    public class TestService
    {
        private readonly ApiClient _api;

        public TestService(ApiClient api)
        {
            _api = api;
        }

        public async Task<string> PingAsync()
        {
            // GET https://yks-tayfa-api.onrender.com/api/test/ping
            return await _api.Http.GetStringAsync("/api/test/ping");
        }
    }
}
