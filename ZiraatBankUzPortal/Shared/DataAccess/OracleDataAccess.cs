using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ZiraatBankUzPortal.Shared.DataAccess
{
    public class OracleDataAccess : IOracleDataAccess
    {
        private readonly IConfiguration _config;


        public OracleDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IList<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (var connection = new OracleConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var rows = await connection.QueryAsync<T>(storedProcedure,
                                                          parameters,
                                                          commandType: CommandType.StoredProcedure);
                return rows.ToList();
            }
        }  

        public async Task<int> SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return await connection.ExecuteAsync(storedProcedure,parameters,commandType: CommandType.StoredProcedure);
            }
        }
        


    }

}
