using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiraatBankUzPortal.Shared.DataAccess
{
    public class DataOracleParameters
    {
        public OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
        public List<OracleDynamicParameters> oracleParameters = new List<OracleDynamicParameters>();


        public void Add(string name, OracleMappingType oracleMappingDbType, ParameterDirection direction, object? value = null, int? size = null)
        {
            
            if (size.HasValue)
            {
                dynamicParameters.Add(name: name, dbType: oracleMappingDbType, direction: direction, value: value, size: size);
            }
            else
            {
                dynamicParameters.Add(name: name, dbType: oracleMappingDbType, direction: direction, value: value);
            }       
        }

        public void Add(string name, OracleMappingType oracleMappingDbType, ParameterDirection direction)
        {
            dynamicParameters.Add(name: name, dbType: oracleMappingDbType, direction: direction);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            var oracleCommand = command as OracleCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }
    }
}
