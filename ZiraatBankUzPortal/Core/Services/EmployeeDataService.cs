using Dapper.Oracle;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Core.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public EmployeeDataService(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }

        public async Task CreateEmployee(EmployeeCreateDto employeeModel)
        {
            Utils.NLogMessage(GetType(), $"{"CreateEmployee()"}", Utils.NLogType.Info);
            try
            {
                DataOracleParameters parameters = new DataOracleParameters();
                parameters.Add("p_FIRSTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.Firstname);
                parameters.Add("p_LASTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.Lastname);
                parameters.Add("P_TITLEID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.TitleId);
                parameters.Add("p_POSITIONID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.PositionId);
                parameters.Add("p_LOCATIONID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.LocationId);
                parameters.Add("p_IPT", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.IPT);
                parameters.Add("p_CELLPHONE", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.CellPhone);
                parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.RecordUser);
                parameters.Add("p_DATEOFBIRTH", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.DateofBirth.HasValue ? employeeModel.DateofBirth.Value.ToString("yyyyMMdd") : "");
                parameters.Add("p_PICTURE", OracleMappingType.Blob, ParameterDirection.Input, employeeModel.Picture);
                await _orcaleDataAccess.SaveDataAsync("GEN_INS_EMPLOYEE_SP", parameters.dynamicParameters, "OracleConnectionString");
            }
            catch (Exception e)
            {
                Utils.NLogMessage(GetType(), $" {e.Message} - {e.InnerException}", Utils.NLogType.Error);
            }
        }

        public async Task<IEnumerable<EmployeeDisplayModel>> GetAllEmployeeAsync()
        {
            Utils.NLogMessage(GetType(), $"{"GetAllUserAsync()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<EmployeeDisplayModel, dynamic>("GEN_SEL_EMPLOYEES_SP", parameters.dynamicParameters, "OracleConnectionString");
            return user;
        }


        public async Task<EmployeeDisplayModel> GetEmployeeByIdAsync(int UserId)
        {
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_Id", OracleMappingType.Int32, ParameterDirection.Input, UserId);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<EmployeeDisplayModel, dynamic>("GEN_SEL_EMPLOYEEID_SP", parameters.dynamicParameters, "OracleConnectionString");
            return user.FirstOrDefault();
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employeeModel)
        {
            Utils.NLogMessage(GetType(), $"{"UpdateEmployee()"}", Utils.NLogType.Info);
            try
            {
                DataOracleParameters parameters = new DataOracleParameters();
                parameters.Add("p_ID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.Id);
                parameters.Add("p_FIRSTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.Firstname);
                parameters.Add("p_LASTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.Lastname);
                parameters.Add("P_TITLEID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.TitleId);
                parameters.Add("p_POSITIONID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.PositionId);
                parameters.Add("p_LOCATIONID", OracleMappingType.Int32, ParameterDirection.Input, employeeModel.LocationId);
                parameters.Add("p_IPT", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.IPT);
                parameters.Add("p_CELLPHONE", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.CellPhone);
                parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.RecordUser);
                parameters.Add("p_DATEOFBIRTH", OracleMappingType.Varchar2, ParameterDirection.Input, employeeModel.DateofBirth.HasValue ? employeeModel.DateofBirth.Value.ToString("yyyyMMdd") : "");
                parameters.Add("p_PICTURE", OracleMappingType.Blob, ParameterDirection.Input, employeeModel.Picture);
                await _orcaleDataAccess.SaveDataAsync("GEN_UPD_EMPLOYEE_SP", parameters.dynamicParameters, "OracleConnectionString");
            }
            catch (Exception e)
            {
                Utils.NLogMessage(GetType(), $" {e.Message} - {e.InnerException}", Utils.NLogType.Error);
            }
        }

        public async Task DeleteEmployee(int employeeId)
        {
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_ID", OracleMappingType.Int32, ParameterDirection.Input, employeeId);
            parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, "1");
            await _orcaleDataAccess.SaveDataAsync("GEN_DEL_EMPLOYEE_SP", parameters.dynamicParameters, "OracleConnectionString");
        }

        public async Task<LoginUserDisplayModel> GetLoginEmployeeByIdAsync(string userName, string password)
        {
            LoginUserDisplayModel loginUser = new LoginUserDisplayModel();
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_USERNAME", OracleMappingType.Varchar2, ParameterDirection.Input, userName);
            parameters.Add("p_PASSWORD", OracleMappingType.Varchar2, ParameterDirection.Input, password);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<LoginUserDisplayModel, dynamic>("GEN_SEL_BLAZORLOGINID_SP", parameters.dynamicParameters, "OracleConnectionString");
            if(user.Count !=0) user.First().AccessToken = GenerateAccessToken(user.First().EmployeeId, user.First().UserName, user.First().RoleName); 
            return user.FirstOrDefault();
        }
        private string GenerateAccessToken(string userId, string userName, string role)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["JwtSettings:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, userId),
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Role, role)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["JwtSettings:Issuer"],
                _config["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signIn);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
        public async Task<IEnumerable<EmployeeTitleDto>> GetEmployeeTitleComboBoxData()
        {
            Utils.NLogMessage(GetType(), $"{"GetEmployeeTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<EmployeeTitleDto, dynamic>("GEN_SEL_EMPLOYEETITLES_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }

        public async Task<IEnumerable<EmployeePositionDto>> GetEmployeePositionComboBoxData()
        {
            Utils.NLogMessage(GetType(), $"{"GetEmployeeTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<EmployeePositionDto, dynamic>("GEN_SEL_EMPLOYEEPOSITIONS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }

        public async Task<IEnumerable<EmployeeLocationDto>> GetEmployeeLocationComboBoxData()
        {
            Utils.NLogMessage(GetType(), $"{"GetEmployeeTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<EmployeeLocationDto, dynamic>("GEN_SEL_EMPLOYEELOCATIONS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }
        public async Task<IEnumerable<EmployeeDepartmentDto>> GetEmployeeDepartmentComboBoxData()
        {
            Utils.NLogMessage(GetType(), $"{"GetEmployeeDepartmentComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<EmployeeDepartmentDto, dynamic>("GEN_SEL_EMPLOYEEDEPARTMENTS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }
    }
}
