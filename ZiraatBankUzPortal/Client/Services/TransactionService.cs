using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Headers;
using ZiraatBankUzPortal.Client.Contracts;
using ZiraatBankUzPortal.Client.Pages;

namespace ZiraatBankUzPortal.Client.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public TransactionService(ILocalStorageService localStorage, HttpClient http, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task<HttpResponseMessage> DeleteTransaction(int transactionId)
        {
            
            string token = await _localStorage.GetItemAsStringAsync("token");
            token = token.Substring(1, token.Length - 1).Substring(0, token.Length - 2);
            var request = new HttpRequestMessage(HttpMethod.Delete, _http.BaseAddress + "api/Transaction/DeleteTransaction/" + transactionId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.SendAsync(request);
            

        }
    }
}
