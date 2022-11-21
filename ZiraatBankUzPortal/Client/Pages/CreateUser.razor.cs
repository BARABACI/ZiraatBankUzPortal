using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class CreateUser
    {
        [Parameter]
        public int userId { get; set; }
        public string _pageRoles;
        private UserModel user = new UserModel();
        private CreateUserDto createUser = new CreateUserDto();
        private UpdateUserDto updateUser = new UpdateUserDto();
        private IEnumerable<UserTitleDto> userTitleNameList;
        private IEnumerable<UserPositionDto> userPositionNameList;
        private IEnumerable<UserLocationDto> userLocationNameList;
        public DateTime? birthdate { get; set; }
        public string image;
        public byte[] imageByte;
        public EventCallback<byte[]> OnSelectedImage { get; set; }
        public string? imageBase64 { get; set; }
        private IBrowserFile? _file;
        int authId;
        string authName,authRole;

        protected override async Task OnParametersSetAsync()
        {
            authId = await _userService.GetAuthId();
            await GetPageRole();
            await GetUserTitleComboBoxData();
            await GetUserPositionComboBoxData();
            await GetUserLocationComboBoxData();
            if (userId != 0)
            {
                await GetUserInformation();
            }
        }
        public async Task HandleValidSubmit()
        {
            if (Convert.ToInt32(userId) != 0)
            {
                if (imageByte != null)
                {
                    user.Picture = imageByte;
                    updateUser.Picture = user.Picture;
                }

                if (birthdate.HasValue)
                {
                    user.DateofBirth = birthdate;
                    updateUser.DateofBirth = user.DateofBirth;
                }

                updateUser.Id = userId;
                updateUser.Firstname = user.Firstname;
                updateUser.Lastname = user.Lastname;
                updateUser.TitleId = user.TitleId;
                updateUser.PositionId = user.PositionId;
                updateUser.LocationId = user.LocationId;
                updateUser.IPT = user.IPT;
                updateUser.CellPhone = user.CellPhone;
                updateUser.RecordUser = authId.ToString();
                updateUser.Picture = user.Picture;

                _httpResponse = await _userService.UpdateUser(updateUser);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("User updated.", Severity.Success);
                    await GetUserInformation();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "User not updated. ", Severity.Error);
                }
            }
            else
            {
                if (birthdate.HasValue)
                {
                    user.DateofBirth = birthdate;
                    createUser.DateofBirth = user.DateofBirth;
                }

                user.Picture = imageByte;
                createUser.Firstname = user.Firstname;
                createUser.Lastname = user.Lastname;
                createUser.TitleId = user.TitleId;
                createUser.PositionId = user.PositionId;
                createUser.LocationId = user.LocationId;
                createUser.IPT = user.IPT;
                createUser.CellPhone = user.CellPhone;
                createUser.RecordUser = authId.ToString();
                createUser.Picture = user.Picture;
                _httpResponse = await _userService.CreateUser(createUser);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("User created.", Severity.Success);
                    user = new UserModel();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "User not created.", Severity.Error);
                }
                
            }
        }
        async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            var extension = Path.GetExtension(_file.Name);
            //var fileName = $"{UserId}-{Guid.NewGuid()}{extension}";
            var format = "image/png";
            var imageFile = await e.File.RequestImageFileAsync(format, 200, 200);
            var buffer = new byte[imageFile.Size];
            await imageFile.OpenReadStream().ReadAsync(buffer);
            imageBase64 = Convert.ToBase64String(buffer);
            image = string.Format("data:image/png;base64,{0}", imageBase64);
            imageByte = buffer;
            StateHasChanged();
        }
        public async Task GetUserInformation()
        {
            _httpResponse = await _userService.GetUserById(userId);
            if (_httpResponse.IsSuccessStatusCode)
            {
                user = await _httpResponse.Content.ReadFromJsonAsync<UserModel>();
                birthdate = user.DateofBirth;
                if (user.Picture != null)
                {
                    imageBase64 = Convert.ToBase64String(user.Picture);
                    image = string.Format("data:image/png;base64,{0}", imageBase64);
                }
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "User information not loaded.", Severity.Error);
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
        private async Task GetUserTitleComboBoxData()
        {
            _httpResponse = await _userService.GetUserTitleComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                userTitleNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<UserTitleDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Title names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchUserTitleComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return userTitleNameList.Select(x => x.ID);

            return userTitleNameList.Where(x => x.TITLE.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
        private async Task GetUserPositionComboBoxData()
        {
            _httpResponse = await _userService.GetUserPositionComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                userPositionNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<UserPositionDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Position names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchUserPositionComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return userPositionNameList.Select(x => x.ID);

            return userPositionNameList.Where(x => x.POSITION.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
        private async Task GetUserLocationComboBoxData()
        {
            _httpResponse = await _userService.GetUserLocationComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                userLocationNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<UserLocationDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Location names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchUserLocationComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return userLocationNameList.Select(x => x.ID);

            return userLocationNameList.Where(x => x.LOCATION.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
    }
}