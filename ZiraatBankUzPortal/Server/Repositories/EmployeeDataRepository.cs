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
        private readonly IEmployeeDataService _employeeDataService;
        public EmployeeDataRepository(IConfiguration config, IEmployeeDataService employeeDataService)
        {
            _config = config;
            _employeeDataService = employeeDataService;
        }

        public async Task CreateEmployee(EmployeeCreateDto employeeModel)
        {
            await _employeeDataService.CreateEmployee(employeeModel);
        }

        public async Task<IEnumerable<EmployeeDisplayModel>> GetAllEmployeeAsync()
        {
            var user = await _employeeDataService.GetAllEmployeeAsync();
            return user;
        }


        public async Task<EmployeeDisplayModel> GetEmployeeByIdAsync(int employeId)
        {
            var user = await _employeeDataService.GetEmployeeByIdAsync(employeId);
            return user;
        }

        public async Task UpdateEmployee(EmployeeUpdateDto employeeModel)
        {
            await _employeeDataService.UpdateEmployee(employeeModel);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _employeeDataService.DeleteEmployee(employeeId);
        }

        public async Task<LoginUserDisplayModel> GetLoginEmployeeByIdAsync(string userName, string password)
        {
            var user = await _employeeDataService.GetLoginEmployeeByIdAsync(userName, password);
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
            var data = await _employeeDataService.GetEmployeeTitleComboBoxData();
            return data;
        }

        public async Task<IEnumerable<EmployeePositionDto>> GetEmployeePositionComboBoxData()
        {
            var data = await _employeeDataService.GetEmployeePositionComboBoxData();
            return data;
        }

        public async Task<IEnumerable<EmployeeLocationDto>> GetEmployeeLocationComboBoxData()
        {
            var data = await _employeeDataService.GetEmployeeLocationComboBoxData();
            return data;
        }
        public async Task<IEnumerable<EmployeeDepartmentDto>> GetEmployeeDepartmentComboBoxData()
        {
            var data = await _employeeDataService.GetEmployeeDepartmentComboBoxData();
            return data;
        }
    }
}
