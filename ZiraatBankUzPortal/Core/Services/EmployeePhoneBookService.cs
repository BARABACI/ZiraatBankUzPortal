using Dapper.Oracle;
using System.Data;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Helper;

namespace ZiraatBankUzPortal.Core.Services
{
    public class EmployeePhoneBookService : IEmployeePhoneBookService
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public EmployeePhoneBookService(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }
        public async Task<IEnumerable<EmployeePhoneBookDisplayModel>> GetAllEmployeePhoneBookAsync()
        {
            Utils.NLogMessage(GetType(), $"{"GetAllEmployeePhoneBookAsync()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var phonebook = await _orcaleDataAccess.LoadDataAsync<EmployeePhoneBookDisplayModel, dynamic>("GEN_SEL_EMPLOYEESPHONEBOOK_SP", parameters.dynamicParameters, "OracleConnectionString");
            return phonebook;
        }
    }
}
