using Oracle.ManagedDataAccess.Client;

namespace ZiraatBankUzPortal.Shared.DataAccess
{
    public interface IOracleDataAccess
    {
        Task<IList<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<int> SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName);
        
    }
}