using Dapper.Oracle;
using System.Collections;
using System.Data;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Core.Services
{
    public class InternalExportExcelService : IInternalExportExcelService
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public InternalExportExcelService(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }
        public async Task<IEnumerable<InternalClientPDisplayModel>> GetDataClientPAsync()
        {
            Utils.NLogMessage(GetType(), $"{"GetAllDataClientP()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<InternalClientPDisplayModel, dynamic>("GEN_SEL_INTERNALEXCELCLIENTP_SP", parameters.dynamicParameters, "Connection395");
            return data;
        }
        public async Task<IEnumerable<InternalClientP1DisplayModel>> GetDataClientP1Async(string startdate, string enddate)
        {
            Utils.NLogMessage(GetType(), $"{"GetAllDataClientP1()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("STARTDATE", OracleMappingType.Varchar2, ParameterDirection.Input, startdate);
            parameters.Add("ENDDATE", OracleMappingType.Varchar2, ParameterDirection.Input, enddate);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<InternalClientP1DisplayModel, dynamic>("GEN_SEL_INTERNALEXCELCLIENTP1_SP", parameters.dynamicParameters, "Connection395");
            return data;
        }
        public async Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArghAsync(string enddate)
        {
            Utils.NLogMessage(GetType(), $"{"GetDataGenerealArghAsync()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("ENDDATE", OracleMappingType.Varchar2, ParameterDirection.Input, enddate);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<InternalGeneralArghDisplayModel, dynamic>("GEN_SEL_INTERNALEXCELGENERALARH_SP", parameters.dynamicParameters, "Connection395");
            return data;
        }
        public async Task<IEnumerable<InternalGeneralArghDisplayModel>> GetDataGenerealArgh1Async(string enddate)
        {
            Utils.NLogMessage(GetType(), $"{"GetDataGenerealArgh1Async()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("ENDDATE", OracleMappingType.Varchar2, ParameterDirection.Input, enddate);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<InternalGeneralArghDisplayModel, dynamic>("GEN_SEL_INTERNALEXCELGENERALARH1_SP", parameters.dynamicParameters, "Connection395");
            return data;
        }
    }
}
