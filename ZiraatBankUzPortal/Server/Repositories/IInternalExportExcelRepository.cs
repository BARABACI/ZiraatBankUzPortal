using System.Collections;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface IInternalExportExcelRepository
    {
        Task<IEnumerable<InternalClientPDisplayModel>> GetDataClientPAsync();
        Task<IEnumerable<InternalClientP1DisplayModel>> GetDataClientP1Async(string startdate, string enddate);
        Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArghAsync(string enddate);
        Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArgh1Async(string enddate);
    }
}