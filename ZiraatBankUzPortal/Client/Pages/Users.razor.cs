using MudBlazor;
using System.Net.Http.Json;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class Users
    {
        private IEnumerable<DisplayUserModel>? user;
        public string _pageRoles;
        private string _searchString = "";
        private bool _canExportUsers;
        protected override async Task OnInitializedAsync()
        {
            await GetPageRole();
            await GetAllUser();
        }
        private bool Search(DisplayUserModel user)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (user.Firstname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (user.Lastname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (user.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (user.Position?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (user.IPT?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;
        }
        private async Task EditUserModal(int UserId)
        {
            var parameters = new DialogParameters { { nameof(CreateUser.userId), UserId } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, DisableBackdropClick = true };
            var dialog = _dialogService.Show<CreateUser>("Edit User", parameters, options);
            var result = await dialog.Result;
            if (result.Cancelled)
            {
                await GetAllUser();
            }
        }    
        private async Task GetPageRole()
        {
            _httpResponse = await _pageRoleService.GetPageRoles(_navigationManager.ToBaseRelativePath(_navigationManager.Uri));
            if (_httpResponse.IsSuccessStatusCode)
            {
                _pageSetting = await _httpResponse.Content.ReadFromJsonAsync<MenuModel>();
                _pageRoles = _pageSetting.PageRoles;
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Page roles not loaded.", Severity.Error);
            }
        }
        private async Task GetAllUser()
        {
            _httpResponse = await _userService.GetAllUser();
            if (_httpResponse.IsSuccessStatusCode)
            {
                user = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<DisplayUserModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Users not loaded.", Severity.Error);
            }
        }
        private async Task DeleteUserModal(int UserId)
        {
            var parameters = new DialogParameters { { nameof(DeleteDialog.ContentText), string.Format("Do you want delete the user with Id " + UserId) } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Delete User", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                _httpResponse = await _userService.DeleteUser(UserId);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("User deleted.", Severity.Success);
                    await GetAllUser();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "User not deleted.", Severity.Error);
                }
            }
        }
    }
}