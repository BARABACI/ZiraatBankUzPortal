using Dapper.Oracle;
using System.Data;
using ZiraatBankUzPortal.Shared.DataAccess;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public TransactionRepository(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }
        public async Task DeleteTransaction(int transactionNo)
        {
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_trid", OracleMappingType.Int32, ParameterDirection.Input, transactionNo);
            await _orcaleDataAccess.SaveDataAsync("gen_upt_transaction_sp", parameters.dynamicParameters, "Connection395");
        }
    }
}
