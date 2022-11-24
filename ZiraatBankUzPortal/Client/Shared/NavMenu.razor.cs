using System.Net.Http.Json;
using System.Security.Claims;
using ZiraatBankUzPortal.Client.Extensions;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Shared
{
    public partial class NavMenu
    {
        string userRole;
        private IEnumerable<MenuModel>? menuList;
        protected override async Task OnInitializedAsync()
        {
            await LoadMenuDataAsync();
        }

        private async Task LoadMenuDataAsync()
        {
            var state = await _authStateProvider.GetAuthenticationStateAsync();
            var user = new ClaimsPrincipal(state.User.Identity);
            if (user.Identity.IsAuthenticated)
            {
                userRole = user.GetRole();
                menuList = await Http.GetFromJsonAsync<IEnumerable<MenuModel>>("api/Menu/Getallmenu?api-version=1");
            }
        }
    }
}