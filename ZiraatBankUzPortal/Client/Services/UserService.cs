using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ZiraatBankUzPortal.Client.Contracts;
using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Client.Services
{
    public class UserService : IUserService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        public UserService(ILocalStorageService localStorage, HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _http = http;
            _authStateProvider = authStateProvider;
        }
        public async Task<HttpResponseMessage> GetAllUser()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/User/Getall");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> DeleteUser(int userId)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Delete, _http.BaseAddress + "api/User/DeleteUser/" + userId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> UpdateUser(UpdateUserDto updateUser)
        {
            var data = JsonConvert.SerializeObject(updateUser);
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Post, _http.BaseAddress + "api/User/UpdateUser");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> CreateUser(CreateUserDto createUser)
        {
            var data = JsonConvert.SerializeObject(createUser);
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Post, _http.BaseAddress + "api/User/CreateUser");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetUserById(int userId)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/User/GetUserById/" + userId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetUserTitleComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/User/GetUserTitleComboBoxData");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetUserPositionComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/User/GetUserPositionComboBoxData");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetUserLocationComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/User/GetUserLocationComboBoxData");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<int>GetAuthId()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);

            return Convert.ToInt32(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        public async Task<string> GetAuthName()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);

            return user.FindFirst(ClaimTypes.Name).Value;
        }
        public async Task<string> GetAuthRole()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);

            return user.FindFirst(ClaimTypes.Role).Value;
        }
    }
}
