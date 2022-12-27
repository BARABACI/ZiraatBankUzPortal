using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Core.Services
{
    public interface IEmployeePhoneBookService
    {
        Task<IEnumerable<EmployeePhoneBookDisplayModel>> GetAllEmployeePhoneBookAsync();
    }
}