using MudBlazor;
using System.Net.Http.Json;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class PhoneBook
    {
        private IEnumerable<EmployeePhoneBookDisplayModel>? phonebook;
        private string _searchString = "";
        protected override async Task OnInitializedAsync()
        {
            await GetAllEmployee();
        }
        private async Task ReloadDataAsync()
        {
            await GetAllEmployee();
        }
        private bool Search(EmployeePhoneBookDisplayModel phonebook)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (phonebook.Firstname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (phonebook.Lastname?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (phonebook.Title?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (phonebook.Position?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (phonebook.Location?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            if (phonebook.IPT?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (phonebook.CellPhone?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
        private async Task GetAllEmployee()
        {
            _httpResponse = await _employeePhoneBookService.GetAllEmployeePhoneBook();
            if (_httpResponse.IsSuccessStatusCode)
            {
                phonebook = await _httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeePhoneBookDisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Employee phone book not loaded.", Severity.Error);
            }
        }
        private void ExportDataToExcel()
        {
            _excelService.ExportDataToTemplateExcel("templates/exceltemplates/EmployeePhoneBookTemplate.xlsx","item", phonebook, "export.xlsx");           
        }
    }
}