using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using ZiraatBankUzPortal.Client.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using ZiraatBankUzPortal.Shared;
using Blazored.LocalStorage;
using System.Security.Claims;
using Microsoft.Extensions.Localization;
using MudBlazor;
using ZiraatBankUzPortal.Shared.Model;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Constants;
using ZiraatBankUzPortal.Client.Extensions;
using ZiraatBankUzPortal.Client.Contracts;
using BlazorComponents.Shared;
using BlazorComponents.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using ClosedXML.Excel;
using MudBlazor.Extensions;
using System.Globalization;

namespace ZiraatBankUzPortal.Client.Pages.InternalControlDep
{
    public partial class Aml
    {
        private List<InternalClientPDisplayModel> clientPModel;
        private List<InternalClientP1DisplayModel> clientP1Model;
        private List<InternalGeneralArghDisplayModel> generalArghModel;
        private List<InternalGeneralArghDisplayModel> generalArgh1Model;
        public string _pageRoles;
        public int waitScreen = 0;
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetPageRole();
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
        private async Task GetDataClientPAsync()
        {
            _httpResponse = await _InternalExportExcelService.GetDataClientPAsync();
            if (_httpResponse.IsSuccessStatusCode)
            {
                clientPModel = await _httpResponse.Content.ReadFromJsonAsync<List<InternalClientPDisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Client_p not loaded.", Severity.Error);
            }
        }
        private async Task GetDataClientP1Async()
        {
            if (startdate != null && enddate != null)
            {
                _httpResponse = await _InternalExportExcelService.GetDataClientP1Async(startdate.Value.ToString("dd.MM.yyyy"), enddate.Value.ToString("dd.MM.yyyy"));
            }

            if (_httpResponse.IsSuccessStatusCode)
            {
                clientP1Model = await _httpResponse.Content.ReadFromJsonAsync<List<InternalClientP1DisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "Client_p1 not loaded.", Severity.Error);
            }
        }
        private async Task GetDataGeneralArghAsync()
        {
            if (enddate != null)
            {
                _httpResponse = await _InternalExportExcelService.GetDataGeneralArghAsync(enddate.Value.ToString("dd.MM.yyyy"));
            }
            if (_httpResponse.IsSuccessStatusCode)
            {
                generalArghModel = await _httpResponse.Content.ReadFromJsonAsync<List<InternalGeneralArghDisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "General_argh not loaded.", Severity.Error);
            }
        }
        private async Task GetDataGeneralArgh1Async()
        {
            if (enddate != null)
            {
                _httpResponse = await _InternalExportExcelService.GetDataGeneralArgh1Async(enddate.Value.ToString("dd.MM.yyyy"));
            }
            if (_httpResponse.IsSuccessStatusCode)
            {
                generalArgh1Model = await _httpResponse.Content.ReadFromJsonAsync<List<InternalGeneralArghDisplayModel>>();
            }
            else
            {
                _snackBar.Add("(" + _httpResponse.StatusCode + ")" + "General_argh1 not loaded.", Severity.Error);
            }
        }
        private async void ExportDataToExcel()
        {
            if (enddate != null && startdate != null)
            {
                waitScreen = 1;
                await GetDataClientPAsync();
                await GetDataClientP1Async();
                await GetDataGeneralArghAsync();
                await GetDataGeneralArgh1Async();
                if (clientPModel.Count != 0)
                {
                    _excelService.ExportDataToCustomizeExcelAddWorkSheet(clientPModel, "client_p");
                }
                if (clientP1Model.Count != 0)
                {
                    _excelService.ExportDataToCustomizeExcelAddWorkSheet(clientP1Model, "client_p1");
                }
                if (generalArghModel.Count != 0)
                {
                    _excelService.ExportDataToCustomizeExcelAddWorkSheet(generalArghModel, "general_argh");
                }
                if (generalArgh1Model.Count != 0)
                {
                    _excelService.ExportDataToCustomizeExcelAddWorkSheet(generalArgh1Model, "general_argh1");
                }
                if (clientPModel.Count != 0 || clientP1Model.Count != 0 || generalArghModel.Count != 0 || generalArgh1Model.Count != 0)
                {
                    _excelService.ExportExcelFile();
                }
                waitScreen = 0;
            }
            else
            {
                if (startdate == null) _snackBar.Add("Startdate field cannot be empty.", Severity.Error);
                if (enddate == null) _snackBar.Add("Enddate field cannot be empty", Severity.Error);
            }
            StateHasChanged();
        }
    }
}