using System.Security.Claims;
using ZiraatBankUzPortal.Client.Extensions;

namespace ZiraatBankUzPortal.Client.Shared
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        private bool _rightToLeft = false;
        string userId, userName, userRole;
        protected override async Task OnInitializedAsync()
        {
            await LoadUserDataAsync();
        }

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task LoadUserDataAsync()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);
            if (user.Identity.IsAuthenticated)
            {
                userId = user.GetId();
                userName = user.GetName();
                userRole = user.GetRole();
            }
            else
            {
                _navigationManager.NavigateTo("/Login");
            }
        }
    }
}