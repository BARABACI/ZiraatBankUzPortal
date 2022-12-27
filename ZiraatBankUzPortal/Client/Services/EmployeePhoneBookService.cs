using System.Net.Http.Headers;
using ZiraatBankUzPortal.Client.Contracts;

namespace ZiraatBankUzPortal.Client.Services
{
    public class EmployeePhoneBookService : IEmployeePhoneBookService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public EmployeePhoneBookService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public async Task<HttpResponseMessage> GetAllEmployeePhoneBook()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/EmployeePhoneBook/Getall?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
    }
}
