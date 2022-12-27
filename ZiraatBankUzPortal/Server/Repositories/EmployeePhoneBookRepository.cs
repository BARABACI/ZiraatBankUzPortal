using ZiraatBankUzPortal.Core.Services;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class EmployeePhoneBookRepository : IEmployeePhoneBookRepository
    {
        private readonly IConfiguration _config;
        private readonly IEmployeePhoneBookService _employeePhoneBookService;
        public EmployeePhoneBookRepository(IConfiguration config, IEmployeePhoneBookService employeePhoneBookService)
        {
            _config = config;
            _employeePhoneBookService = employeePhoneBookService;
        }
        public async Task<IEnumerable<EmployeePhoneBookDisplayModel>> GetAllEmployeePhoneBookAsync()
        {
            var phonebook = await _employeePhoneBookService.GetAllEmployeePhoneBookAsync();
            return phonebook;
        }
    }
}
