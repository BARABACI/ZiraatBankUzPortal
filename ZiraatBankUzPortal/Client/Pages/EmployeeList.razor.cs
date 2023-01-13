using MudBlazor;
using System.Net.Http.Json;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class EmployeeList
    {
        private IEnumerable<EmployeeDisplayModel>? employee;
        public string _pageRoles;
        private string _searchString = "";
        private bool _canExportUsers;
        protected override async Task OnInitializedAsync()
        {
            await GetPageRole();
            await GetAllEmployee();
        }
        private async Task ReloadDataAsync()
        {
            await GetAllEmployee();
        }
        private bool Search(EmployeeDisplayModel employee)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (employee.Firstname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (employee.Lastname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (employee.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (employee.Position?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (employee.IPT?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;
        }
        private async Task EditEmployeeModal(int employeId)
        {
            var parameters = new DialogParameters { { nameof(EmployeeCreate.employeeId), employeId } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, DisableBackdropClick = true };
            var dialog = _dialogService.Show<EmployeeCreate>(_localizer["ModalHeaderEditEmployeeText"], parameters, options);
            var result = await dialog.Result;
            if (result.Cancelled)
            {
                await GetAllEmployee();
            }
        }
        private async Task CreateEmployeeModal()
        {
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, DisableBackdropClick = true };
            var dialog = _dialogService.Show<EmployeeCreate>(_localizer["ModalHeaderCreateEmployeeText"], options);
            var result = await dialog.Result;
            if (result.Cancelled)
            {
                await GetAllEmployee();
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
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + _localizer["PageRoleNotLoadedText"], Severity.Error);
            }
        }
        private async Task GetAllEmployee()
        {
            _httpResponse = await _employeeService.GetAllEmployee();
            if (_httpResponse.IsSuccessStatusCode)
            {
                employee = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeDisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + _localizer["EmployeDataNotLoadedText"], Severity.Error);
            }
        }
        private async Task DeleteEmployeeModal(int employeId)
        {
            var parameters = new DialogParameters { { nameof(DeleteDialog.ContentText), string.Format(_localizer["ModalContentDeleteEmployeeText"] + employeId + ")") } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>(_localizer["ModalHeaderDeleteEmployeeText"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                _httpResponse = await _employeeService.DeleteEmployee(employeId);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add(_localizer["SuccDeleteEmployeeText"], Severity.Success);
                    await GetAllEmployee();
                }
                else
                {
                    _snackBar.Add("(" + _httpResponse.StatusCode + ")" + _localizer["ErrDeleteEmployeeText"], Severity.Error);
                }
            }
        }
    }
}