using BlazorComponents.Shared;
using ClosedXML.Excel;
using ClosedXML.Report;
using ClosedXML.Report.Utils;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Reflection;
using ZiraatBankUzPortal.Client.Contracts;
using ZiraatBankUzPortal.Shared.DisplayModel;
using static System.Net.WebRequestMethods;

namespace ZiraatBankUzPortal.Client.Services
{
    public class ExcelService : IExcelService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        XLWorkbook wb = new XLWorkbook();
        public ExcelService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }
        public async void ExportDataToTemplateExcel(string templateURL, string excelDataName,object data, string outputfilename)
        {
            Stream streamTemplate = await _http.GetStreamAsync(templateURL);
            var template = new XLTemplate(streamTemplate);
            template.AddVariable(excelDataName, data);
            template.Generate();
            MemoryStream XLSStream = new();
            template.SaveAs(XLSStream);
            await _js.InvokeVoidAsync("BlazorDownloadFile", outputfilename, XLSStream.ToArray());
        }

        public void ExportDataToCustomizeExcelAddWorkSheet<T>(List<T> data, string excelWorkSheetName)
        {
            var ws = wb.Worksheets.Add(excelWorkSheetName);

            var props = data[0].GetType().GetProperties().Select(k => k.Name).ToList();
            int colnum = 1;
            List<string> columname = new List<string>();
            foreach (var column in props)
            {
                ws.Cell(1, colnum).Value = column;
                columname.Add(column);
                colnum++;
            }
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < columname.Count; j++)
                {
                    var value = data[i].GetType().GetProperty(columname[j]).GetValue(data[i], null);
                    if(value != null && value.ToString().IsNumber() && (value.ToString().Length > 18 || value.ToString().Substring(0,1) =="0"))
                    {
                        ws.Cell(i + 2, j + 1).Value = "'" + value;
                    }
                    else
                    {
                        ws.Cell(i + 2, j + 1).Value = value;
                    }              
                }
            }          
        }
        public async void ExportExcelFile()
        {
            MemoryStream XLSStream = new();
            wb.SaveAs(XLSStream);
            await _js.InvokeVoidAsync("BlazorDownloadFile", "export.xlsx", XLSStream.ToArray());
        }
    }
}
