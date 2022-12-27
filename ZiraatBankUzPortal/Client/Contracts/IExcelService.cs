using ClosedXML.Excel;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IExcelService
    {
        void ExportDataToCustomizeExcelAddWorkSheet<T>(List<T> data, string excelWorkSheetName);
        void ExportDataToTemplateExcel(string templateURL, string excelDataName, object data, string outputfilename);
        void ExportExcelFile();
    }
}