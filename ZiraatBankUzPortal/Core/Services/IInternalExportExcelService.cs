using System.Collections;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Core.Services
{
    public interface IInternalExportExcelService
    {
        Task<IEnumerable<InternalClientPDisplayModel>> GetDataClientPAsync();
        Task<IEnumerable<InternalClientP1DisplayModel>> GetDataClientP1Async(string startdate, string enddate);
        Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArghAsync(string enddate);
        Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArgh1Async(string enddate);
    }
}