using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ZiraatBankUzPortal.Client.Contracts;
using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        public EmployeeService(ILocalStorageService localStorage, HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _localStorage = localStorage;
            _http = http;
            _authStateProvider = authStateProvider;
        }
        public async Task<HttpResponseMessage> GetAllEmployee()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Employee/Getall?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> DeleteEmployee(int employeeId)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Delete, _http.BaseAddress + "api/Employee/DeleteEmployee/" + employeeId+ "?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> UpdateEmployee(EmployeeUpdateDto updateEmployee)
        {
            var data = JsonConvert.SerializeObject(updateEmployee);
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Post, _http.BaseAddress + "api/Employee/UpdateEmployee");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> CreateEmployee(EmployeeCreateDto createEmployee)
        {
            var data = JsonConvert.SerializeObject(createEmployee);
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Post, _http.BaseAddress + "api/Employee/CreateEmployee");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetEmployeeById(int employeeId)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Employee/GetEmployeeById/" + employeeId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetEmployeeTitleComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Employee/GetEmployeeTitleComboBoxData?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetEmployeePositionComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Employee/GetEmployeePositionComboBoxData?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetEmployeeLocationComboBoxData()
        {
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Get, _http.BaseAddress + "api/Employee/GetEmployeeLocationComboBoxData?api-version=1");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
        }
        public async Task<int> GetAuthId()
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
