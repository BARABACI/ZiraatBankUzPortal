using DocumentFormat.OpenXml.Spreadsheet;
using ZiraatBankUzPortal.Client.Contracts;

namespace ZiraatBankUzPortal.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _http;

        public LoginService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> Login(string userName, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/UserLogin/" + userName + "/" + password);
            return await _http.SendAsync(request);
        }
    }
}
