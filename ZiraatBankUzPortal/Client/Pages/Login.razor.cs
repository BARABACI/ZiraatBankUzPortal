using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class Login
    {
        public string loginAlertMessage;
        public Boolean loginAlertMessageVisible;
        LoginUserDisplayModel user = new LoginUserDisplayModel();
        protected override async Task OnInitializedAsync()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);
            if (user.Identity.IsAuthenticated)
            {
                _navigationManager.NavigateTo("/");
            }
        }
        async Task HandleLogin()
        {

            user = await Http.GetFromJsonAsync<LoginUserDisplayModel>("api/UserLogin/" + user.UserName + "/" + user.Password);
            if (user.UserName != "" && user.Password != "")
            {
                var token = user.AccessToken;
                await _localStorage.SetItemAsync("token", token);
                await _authStateProvider.GetAuthenticationStateAsync();
                _navigationManager.NavigateTo("/");
            }
            /*
            _httpResponse = await _loginService.Login(userName, password);
            if (_httpResponse.IsSuccessStatusCode)
            {
                user = await _httpResponse.Content.ReadFromJsonAsync<LoginUserDisplayModel>();
                var token = user.AccessToken;
                await _localStorage.SetItemAsync("token", token);
                await _authStateProvider.GetAuthenticationStateAsync();
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Page roles not loaded.", Severity.Error);
            }*/
        }
        private async Task UserLogin(string userName, string password)
        {

        }
    }
}