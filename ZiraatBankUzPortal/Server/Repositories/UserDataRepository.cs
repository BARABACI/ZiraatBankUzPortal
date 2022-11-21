using Dapper.Oracle;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZiraatBankUzPortal.Client.Pages;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IOracleDataAccess _orcaleDataAccess;
        private readonly IConfiguration _config;
        public string? imageBase64 { get; set; }
        public UserDataRepository(IOracleDataAccess orcaleDataAccess, IConfiguration config)
        {
            _orcaleDataAccess = orcaleDataAccess;
            _config = config;
        }

        public async Task CreateUser(CreateUserDto user)
        {
            Utils.NLogMessage(this.GetType(), $"{"CreatePerson()"}", Utils.NLogType.Info);
            try
            {
                DataOracleParameters parameters = new DataOracleParameters();
                parameters.Add("p_FIRSTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, user.Firstname);
                parameters.Add("p_LASTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, user.Lastname);
                parameters.Add("P_TITLEID", OracleMappingType.Int32, ParameterDirection.Input, user.TitleId);
                parameters.Add("p_POSITIONID", OracleMappingType.Int32, ParameterDirection.Input, user.PositionId);
                parameters.Add("p_LOCATIONID", OracleMappingType.Int32, ParameterDirection.Input, user.LocationId);
                parameters.Add("p_IPT", OracleMappingType.Varchar2, ParameterDirection.Input, user.IPT);
                parameters.Add("p_CELLPHONE", OracleMappingType.Varchar2, ParameterDirection.Input, user.CellPhone);
                parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, user.RecordUser);
                parameters.Add("p_DATEOFBIRTH", OracleMappingType.Varchar2, ParameterDirection.Input, user.DateofBirth.HasValue ? user.DateofBirth.Value.ToString("yyyyMMdd") : "");
                parameters.Add("p_PICTURE", OracleMappingType.Blob, ParameterDirection.Input, user.Picture);
                await _orcaleDataAccess.SaveDataAsync("GEN_INS_PERSON_SP", parameters.dynamicParameters, "OracleConnectionString");
            }
            catch (Exception e)
            {
                Utils.NLogMessage(this.GetType(), $" {e.Message} - {e.InnerException}", Utils.NLogType.Error);
            }
        }

        public async Task<IEnumerable<DisplayUserModel>> GetAllUserAsync()
        {
            Utils.NLogMessage(this.GetType(), $"{"GetAllUserAsync()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<DisplayUserModel, dynamic>("GEN_SEL_PERSONS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return user;
        }


        public async Task<DisplayUserModel> GetUserByIdAsync(int UserId)
        {
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_Id", OracleMappingType.Int32, ParameterDirection.Input, UserId);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<DisplayUserModel, dynamic>("GEN_SEL_PERSONSID_SP", parameters.dynamicParameters, "OracleConnectionString");
            return user.FirstOrDefault();
        }

        public async Task UpdateUser(UpdateUserDto user)
        {
            Utils.NLogMessage(this.GetType(), $"{"UpdateUser()"}", Utils.NLogType.Info);
            try
            {
                DataOracleParameters parameters = new DataOracleParameters();
                parameters.Add("p_ID", OracleMappingType.Int32, ParameterDirection.Input, user.Id);
                parameters.Add("p_FIRSTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, user.Firstname);
                parameters.Add("p_LASTNAME", OracleMappingType.Varchar2, ParameterDirection.Input, user.Lastname);
                parameters.Add("P_TITLEID", OracleMappingType.Int32, ParameterDirection.Input, user.TitleId);
                parameters.Add("p_POSITIONID", OracleMappingType.Int32, ParameterDirection.Input, user.PositionId);
                parameters.Add("p_LOCATIONID", OracleMappingType.Int32, ParameterDirection.Input, user.LocationId);
                parameters.Add("p_IPT", OracleMappingType.Varchar2, ParameterDirection.Input, user.IPT);
                parameters.Add("p_CELLPHONE", OracleMappingType.Varchar2, ParameterDirection.Input, user.CellPhone);
                parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, user.RecordUser);
                parameters.Add("p_DATEOFBIRTH", OracleMappingType.Varchar2, ParameterDirection.Input, user.DateofBirth.HasValue ? user.DateofBirth.Value.ToString("yyyyMMdd") : "");
                parameters.Add("p_PICTURE", OracleMappingType.Blob, ParameterDirection.Input, user.Picture);
                await _orcaleDataAccess.SaveDataAsync("GEN_UPD_PERSON_SP", parameters.dynamicParameters, "OracleConnectionString");
            }
            catch (Exception e)
            {
                Utils.NLogMessage(this.GetType(), $" {e.Message} - {e.InnerException}", Utils.NLogType.Error);
            }
        }

        public async Task DeleteUser(int UserId)
        {
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_ID", OracleMappingType.Int32, ParameterDirection.Input, UserId);
            parameters.Add("p_RECORDUSER", OracleMappingType.Varchar2, ParameterDirection.Input, "1");
            await _orcaleDataAccess.SaveDataAsync("GEN_DEL_PERSON_SP", parameters.dynamicParameters, "OracleConnectionString");
        }

        public async Task<DisplayLoginUserModel> GetLoginUserByIdAsync(string userName, string password)
        {
            DisplayLoginUserModel loginUser = new DisplayLoginUserModel();
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("p_USERNAME", OracleMappingType.Varchar2, ParameterDirection.Input, userName);
            parameters.Add("p_PASSWORD", OracleMappingType.Varchar2, ParameterDirection.Input, password);
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var user = await _orcaleDataAccess.LoadDataAsync<DisplayLoginUserModel, dynamic>("GEN_SEL_BLAZORLOGINID_SP", parameters.dynamicParameters, "OracleConnectionString");

            foreach (var p in user)
            {
                loginUser.UserName = p.UserName;
                loginUser.Password = p.Password;
                loginUser.UserId = p.UserId;
                loginUser.RoleName = p.RoleName;
                loginUser.AccessToken = GenerateAccessToken(p.UserId, p.UserName, p.RoleName);
            }
            return loginUser;

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
        public async Task<IEnumerable<UserTitleDto>> GetUserTitleComboBoxData()
        {
            Utils.NLogMessage(this.GetType(), $"{"GetUserTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<UserTitleDto, dynamic>("GEN_SEL_PERSONTITLES_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }

        public async Task<IEnumerable<UserPositionDto>> GetUserPositionComboBoxData()
        {
            Utils.NLogMessage(this.GetType(), $"{"GetUserTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<UserPositionDto, dynamic>("GEN_SEL_PERSONPOSITIONS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }

        public async Task<IEnumerable<UserLocationDto>> GetUserLocationComboBoxData()
        {
            Utils.NLogMessage(this.GetType(), $"{"GetUserTitleComboBoxData()"}", Utils.NLogType.Info);
            DataOracleParameters parameters = new DataOracleParameters();
            parameters.Add("RC1", OracleMappingType.RefCursor, ParameterDirection.Output);
            var data = await _orcaleDataAccess.LoadDataAsync<UserLocationDto, dynamic>("GEN_SEL_PERSONLOCATIONS_SP", parameters.dynamicParameters, "OracleConnectionString");
            return data;
        }
    }
}
