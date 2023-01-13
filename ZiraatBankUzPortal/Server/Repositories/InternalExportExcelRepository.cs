using System.Collections;
using System.Collections.Generic;
using ZiraatBankUzPortal.Core.Services;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class InternalExportExcelRepository : IInternalExportExcelRepository
    {
        private readonly IInternalExportExcelService _internalExportExcelService;
        public InternalExportExcelRepository(IInternalExportExcelService internalExportExcelService)
        {
            _internalExportExcelService = internalExportExcelService;
        }
        public async Task<IEnumerable<InternalClientPDisplayModel>> GetDataClientPAsync()
        {
            var data = await _internalExportExcelService.GetDataClientPAsync();
            return data;
        }
        public async Task<IEnumerable<InternalClientP1DisplayModel>> GetDataClientP1Async(string startdate, string enddate)
        {
            var data = await _internalExportExcelService.GetDataClientP1Async(startdate,enddate);
            return data;
        }
        public async Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArghAsync(string enddate)
        {
            var data = await _internalExportExcelService.GetDataGenerealArghAsync(enddate);
            return data;
        }
        public async Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArgh1Async(string enddate)
        {
            var data = await _internalExportExcelService.GetDataGenerealArgh1Async(enddate);
            return data;
        }
    }
}
