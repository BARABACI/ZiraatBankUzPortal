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
        public Boolean loginAlertMessageVisible=false;
        LoginUserDisplayModel? user = new LoginUserDisplayModel();
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
            _httpResponse = await _loginService.Login(user.UserName, user.Password);
            if (_httpResponse.IsSuccessStatusCode)
            {
                if (_httpResponse.Content.ReadAsStringAsync().Result != "")
                {
                    user = await _httpResponse.Content.ReadFromJsonAsync<LoginUserDisplayModel>();
                    var token = user.AccessToken;
                    await _localStorage.SetItemAsync("token", token);
                    await _authStateProvider.GetAuthenticationStateAsync();
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    loginAlertMessageVisible = true;
                    loginAlertMessage= "(" + _httpResponse.StatusCode + ")" + _localizer["ErrIncorrectUserNamePassText"];
                }
            }
            else
            {
                loginAlertMessageVisible = true;
                loginAlertMessage = "(" + _httpResponse.StatusCode + ")" + _localizer["ErrPageRole"];
            }
        }
    }
}