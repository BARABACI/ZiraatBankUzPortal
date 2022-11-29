using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface IEmployeeDataRepository
    {
        Task CreateEmployee(EmployeeCreateDto employeeModel);
        Task DeleteEmployee(int employeeId);
        Task<IEnumerable<EmployeeDisplayModel>> GetAllEmployeeAsync();
        Task<EmployeeDisplayModel> GetEmployeeByIdAsync(int employeId);
        Task<IEnumerable<EmployeeLocationDto>> GetEmployeeLocationComboBoxData();
        Task<IEnumerable<EmployeePositionDto>> GetEmployeePositionComboBoxData();
        Task<IEnumerable<EmployeeTitleDto>> GetEmployeeTitleComboBoxData();
        Task<LoginUserDisplayModel> GetLoginEmployeeByIdAsync(string userName, string password);
        Task UpdateEmployee(EmployeeUpdateDto employeeModel);
    }
}