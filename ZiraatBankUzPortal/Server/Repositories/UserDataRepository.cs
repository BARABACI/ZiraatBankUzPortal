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
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IConfiguration _config;
        private readonly IUserDataService _userDataService;
        public UserDataRepository(IConfiguration config, IUserDataService userDataService)
        {
            _config = config;
            _userDataService = userDataService;
        }

        public async Task CreateUser(CreateUserDto user)
        {
            await _userDataService.CreateUser(user);
        }

        public async Task<IEnumerable<DisplayUserModel>> GetAllUserAsync()
        {
            var user = await _userDataService.GetAllUserAsync();
            return user;
        }


        public async Task<DisplayUserModel> GetUserByIdAsync(int UserId)
        {
            var user = await _userDataService.GetUserByIdAsync(UserId);
            return user;
        }

        public async Task UpdateUser(UpdateUserDto user)
        {
            await _userDataService.UpdateUser(user);
        }

        public async Task DeleteUser(int UserId)
        {
           await _userDataService.DeleteUser(UserId);
        }

        public async Task<DisplayLoginUserModel> GetLoginUserByIdAsync(string userName, string password)
        {
            var user = await _userDataService.GetLoginUserByIdAsync(userName,password);
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
        public async Task<IEnumerable<UserTitleDto>> GetUserTitleComboBoxData()
        {
            var data = await _userDataService.GetUserTitleComboBoxData();
            return data;
        }

        public async Task<IEnumerable<UserPositionDto>> GetUserPositionComboBoxData()
        {
            var data = await _userDataService.GetUserPositionComboBoxData();
            return data;
        }

        public async Task<IEnumerable<UserLocationDto>> GetUserLocationComboBoxData()
        {
            var data = await _userDataService.GetUserLocationComboBoxData();
            return data;
        }
    }
}
