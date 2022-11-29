using Dapper.Oracle;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;
using ZiraatBankUzPortal.Core.Services;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class EmployeeDataRepository : IEmployeeDataRepository
    {
        private readonly IConfiguration _config;
        private readonly IEmployeeDataService _userDataService;
        public EmployeeDataRepository(IConfiguration config, IEmployeeDataService userDataService)
        {
            _config = config;
            _userDataService = userDataService;
        }

        public async Task CreateEmployee(EmployeeCreateDto employeeModel)
        {
            await _userDataService.CreateEmployee(employeeModel);
        }

        public async Task<IEnumerable<EmployeeDisplayModel>> GetAllEmployeeAsync()
        {
            var user = await _userDataService.GetAllEmployeeAsync();
            return user;
        }


        public async Task<EmployeeDisplayModel> GetEmployeeByIdAsync(int employeId)
        {
            var user = await _userDataService.GetEmployeeByIdAsync(employeId);
            return user;
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employeeModel)
        {
            await _userDataService.UpdateEmployee(employeeModel);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _userDataService.DeleteEmployee(employeeId);
        }

        public async Task<LoginUserDisplayModel> GetLoginEmployeeByIdAsync(string userName, string password)
        {
            var user = await _userDataService.GetLoginEmployeeByIdAsync(userName, password);
            return user;

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
            var data = await _userDataService.GetEmployeeTitleComboBoxData();
            return data;
        }

        public async Task<IEnumerable<EmployeePositionDto>> GetEmployeePositionComboBoxData()
        {
            var data = await _userDataService.GetEmployeePositionComboBoxData();
            return data;
        }

        public async Task<IEnumerable<EmployeeLocationDto>> GetEmployeeLocationComboBoxData()
        {
            var data = await _userDataService.GetEmployeeLocationComboBoxData();
            return data;
        }
    }
}
