namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IInternalExportExcelService
    {
        Task<HttpResponseMessage> GetDataClientPAsync();
        Task<HttpResponseMessage> GetDataClientP1Async(string startdate, string enddate);
        Task<HttpResponseMessage> GetDataGeneralArghAsync(string enddate);
        Task<HttpResponseMessage> GetDataGeneralArgh1Async(string enddate);
    }
}