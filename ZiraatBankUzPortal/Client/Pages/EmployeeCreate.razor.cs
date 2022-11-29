using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class EmployeeCreate
    {
        [Parameter]
        public int employeeId { get; set; }
        public string _pageRoles;
        private EmployeeModel employee = new EmployeeModel();
        private EmployeeCreateDto createEmployee = new EmployeeCreateDto();
        private EmployeeUpdateDto updateEmployee = new EmployeeUpdateDto();
        private IEnumerable<EmployeeTitleDto> employeeTitleNameList;
        private IEnumerable<EmployeePositionDto> employeePositionNameList;
        private IEnumerable<EmployeeLocationDto> employeeLocationNameList;
        public DateTime? birthdate { get; set; }
        public string image;
        public byte[] imageByte;
        public EventCallback<byte[]> OnSelectedImage { get; set; }
        public string? imageBase64 { get; set; }
        int authId;
        private string _firstLetterOfName;
        string authName, authRole;
        protected override async Task OnParametersSetAsync()
        {
            authId = await _employeeService.GetAuthId();
            await GetPageRole();
            await GetEmployeeTitleComboBoxData();
            await GetEmployeePositionComboBoxData();
            await GetEmployeeLocationComboBoxData();
            if (employeeId != 0)
            {
                await GetEmployeeInformation();
            }
        }
        public async Task HandleValidSubmit()
        {
            if (Convert.ToInt32(employeeId) != 0)
            {
                if (imageByte == null)
                {
                    await SetDefaultImage();
                    employee.Picture = imageByte;
                    updateEmployee.Picture = employee.Picture;
                }
                else
                {
                    employee.Picture = imageByte;
                    updateEmployee.Picture = employee.Picture;
                }

                if (birthdate.HasValue)
                {
                    employee.DateofBirth = birthdate;
                    updateEmployee.DateofBirth = employee.DateofBirth;
                }

                updateEmployee.Id = employeeId;
                updateEmployee.Firstname = employee.Firstname;
                updateEmployee.Lastname = employee.Lastname;
                updateEmployee.TitleId = employee.TitleId;
                updateEmployee.PositionId = employee.PositionId;
                updateEmployee.LocationId = employee.LocationId;
                updateEmployee.IPT = employee.IPT;
                updateEmployee.CellPhone = employee.CellPhone;
                updateEmployee.RecordUser = authId.ToString();

                _httpResponse = await _employeeService.UpdateEmployee(updateEmployee);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("Employee updated.", Severity.Success);
                    await GetEmployeeInformation();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Employee not updated. ", Severity.Error);
                }
            }
            else
            {
                if (birthdate.HasValue)
                {
                    employee.DateofBirth = birthdate;
                    createEmployee.DateofBirth = employee.DateofBirth;
                }
                if (imageByte == null)
                {
                    await SetDefaultImage();
                    employee.Picture = imageByte;
                    createEmployee.Picture = employee.Picture;
                }
                else
                {
                    employee.Picture = imageByte;
                    createEmployee.Picture = employee.Picture;
                }

                createEmployee.Firstname = employee.Firstname;
                createEmployee.Lastname = employee.Lastname;
                createEmployee.TitleId = employee.TitleId;
                createEmployee.PositionId = employee.PositionId;
                createEmployee.LocationId = employee.LocationId;
                createEmployee.IPT = employee.IPT;
                createEmployee.CellPhone = employee.CellPhone;
                createEmployee.RecordUser = authId.ToString();
                _httpResponse = await _employeeService.CreateEmployee(createEmployee);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("Employee created.", Severity.Success);
                    employee = new EmployeeModel();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Employee not created.", Severity.Error);
                }

            }
        }
        async Task UploadFiles(InputFileChangeEventArgs e)
        {

            var format = "image/png";
            var imageFile = await e.File.RequestImageFileAsync(format, 200, 200);
            var buffer = new byte[imageFile.Size];
            await imageFile.OpenReadStream().ReadAsync(buffer);
            imageBase64 = Convert.ToBase64String(buffer);
            image = string.Format("data:image/png;base64,{0}", imageBase64);
            imageByte = buffer;
            StateHasChanged();
        }
        private async Task SetDefaultImage()
        {
            var imageStream = await _http.GetStreamAsync(_http.BaseAddress + "images/default_profile_image.png");
            BinaryReader br = new BinaryReader(imageStream);
            byte[] buffer = br.ReadBytes((int)imageStream.Length);
            imageByte = buffer;
        }
        public async Task GetEmployeeInformation()
        {
            _httpResponse = await _employeeService.GetEmployeeById(employeeId);
            if (_httpResponse.IsSuccessStatusCode)
            {
                employee = await _httpResponse.Content.ReadFromJsonAsync<EmployeeModel>();
                birthdate = employee.DateofBirth;
                if (employee.Picture != null)
                {
                    imageBase64 = Convert.ToBase64String(employee.Picture);
                    image = string.Format("data:image/png;base64,{0}", imageBase64);
                }
                else
                {
                    if (employee.Firstname.Length > 0)
                    {
                        _firstLetterOfName = employee.Firstname[0].ToString() + employee.Lastname[0].ToString();
                    }
                }
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Employee information not loaded.", Severity.Error);
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
        private async Task GetEmployeeTitleComboBoxData()
        {
            _httpResponse = await _employeeService.GetEmployeeTitleComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                employeeTitleNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeTitleDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Title names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchEmployeeTitleComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return employeeTitleNameList.Select(x => x.ID);

            return employeeTitleNameList.Where(x => x.TITLE.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
        private async Task GetEmployeePositionComboBoxData()
        {
            _httpResponse = await _employeeService.GetEmployeePositionComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                employeePositionNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeePositionDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Position names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchEmployeePositionComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return employeePositionNameList.Select(x => x.ID);

            return employeePositionNameList.Where(x => x.POSITION.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
        private async Task GetEmployeeLocationComboBoxData()
        {
            _httpResponse = await _employeeService.GetEmployeeLocationComboBoxData();
            if (_httpResponse.IsSuccessStatusCode)
            {
                employeeLocationNameList = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeLocationDto>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Location names not loaded.", Severity.Error);
            }
        }
        private async Task<IEnumerable<int>> SearchEmployeeLocationComboBoxData(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
                return employeeLocationNameList.Select(x => x.ID);

            return employeeLocationNameList.Where(x => x.LOCATION.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ID);
        }
    }
}