using ZiraatBankUzPortal.Shared.Model;
using System.Threading.Tasks;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface IUserDataRepository
    {
        Task CreateUser(CreateUserDto user);
        Task DeleteUser(int UserId);
        Task<IEnumerable<DisplayUserModel>> GetAllUserAsync();
        Task<DisplayUserModel> GetUserByIdAsync(int UserId);
        Task UpdateUser(UpdateUserDto user);
        Task<DisplayLoginUserModel> GetLoginUserByIdAsync(string userName, string password);
    }
}