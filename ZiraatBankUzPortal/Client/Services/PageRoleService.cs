using ZiraatBankUzPortal.Client.Contracts;

namespace ZiraatBankUzPortal.Client.Services
{
    public class PageRoleService : IPageRoleService
    {
        private readonly HttpClient _http;

        public PageRoleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> GetPageRoles(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Menu/Getmenubypagelink/" + url);
            return await _http.SendAsync(request);
        }
    }
}
