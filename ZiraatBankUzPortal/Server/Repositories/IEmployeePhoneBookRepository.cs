using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface IEmployeePhoneBookRepository
    {
        Task<IEnumerable<EmployeePhoneBookDisplayModel>> GetAllEmployeePhoneBookAsync();
    }
}