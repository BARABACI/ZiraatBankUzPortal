using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IUserService
    {
        Task<HttpResponseMessage> GetAllUser();
        Task<HttpResponseMessage> DeleteUser(int userId);
        Task<HttpResponseMessage> UpdateUser(UpdateUserDto updateUser);
        Task<HttpResponseMessage> CreateUser(CreateUserDto createUser);
        Task<HttpResponseMessage> GetUserById(int userId);
        Task<HttpResponseMessage> GetUserTitleComboBoxData();
        Task<HttpResponseMessage> GetUserPositionComboBoxData();
        Task<HttpResponseMessage> GetUserLocationComboBoxData();
        Task<int> GetAuthId();
        Task<string> GetAuthName();
        Task<string> GetAuthRole();
    }
}