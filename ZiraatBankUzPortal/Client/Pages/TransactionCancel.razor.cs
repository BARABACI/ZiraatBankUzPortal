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
using ZiraatBankUzPortal.Client;
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
using System.Drawing.Printing;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class TransactionCancel
    {
        private TransactionModel transactionModel = new TransactionModel();
        private string _pageRoles;
        protected override async Task OnInitializedAsync()
        {
            await SetPageRole();
        }
        public async Task HandleValidSubmit()
        {

            var parameters = new DialogParameters { { nameof(DeleteDialog.ContentText), string.Format("Do you want delete the transaction with Id " + (transactionModel.transActionId).ToString()) } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteDialog>("Delete Transaction", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                _httpResponse = await _transactionService.DeleteTransaction(transactionModel.transActionId);
                if (_httpResponse.IsSuccessStatusCode)
                {
                    _snackBar.Add("Transaction deleted.", Severity.Success);
                }
                else
                {
                    _snackBar.Add("Transaction not deleted. (" + _httpResponse.StatusCode + ")", Severity.Error);
                }
            }
         
           
        }
        private async Task SetPageRole()
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
    }
}