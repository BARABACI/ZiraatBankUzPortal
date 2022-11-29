using System.Net.Http.Json;
using System.Security.Claims;
using ZiraatBankUzPortal.Shared.DisplayModel;

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
        }
    }
}