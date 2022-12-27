using System.Net.Http.Headers;
using ZiraatBankUzPortal.Client.Contracts;

namespace ZiraatBankUzPortal.Client.Services
{
    public class InternalExportExcelService : IInternalExportExcelService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public InternalExportExcelService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }
        public async Task<HttpResponseMessage> GetAllDataAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/InternalExportExcel/Getall?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetDataClientPAsync()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/InternalExportExcel/GetClientP?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetDataClientP1Async(string startdate, string enddate)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/InternalExportExcel/GetClientP1?startdate=" + startdate.ToString() + "&enddate=" + enddate.ToString() + "&api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetDataGeneralArghAsync(string enddate)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/InternalExportExcel/GetGeneralArgh?enddate=" + enddate.ToString() + "&api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetDataGeneralArgh1Async(string enddate)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/InternalExportExcel/GetGeneralArgh1?enddate=" + enddate.ToString() + "&api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
    }
}
