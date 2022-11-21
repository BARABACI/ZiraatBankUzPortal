using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ZiraatBankUzPortal.Client.Pages
{
    public partial class DeleteDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public string ContentText { get; set; }

        void Submit()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();
    }
}