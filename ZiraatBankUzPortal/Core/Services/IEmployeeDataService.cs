using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Core.Services
{
    public interface IEmployeeDataService
    {
        Task CreateEmployee(EmployeeCreateDto employeeModel);
        Task DeleteEmployee(int employeeId);
        Task<IEnumerable<EmployeeDisplayModel>> GetAllEmployeeAsync();
        Task<EmployeeDisplayModel> GetEmployeeByIdAsync(int UserId);
        Task<IEnumerable<EmployeeLocationDto>> GetEmployeeLocationComboBoxData();
        Task<IEnumerable<EmployeePositionDto>> GetEmployeePositionComboBoxData();
        Task<IEnumerable<EmployeeTitleDto>> GetEmployeeTitleComboBoxData();
        Task<IEnumerable<EmployeeDepartmentDto>> GetEmployeeDepartmentComboBoxData();
        Task<LoginUserDisplayModel> GetLoginEmployeeByIdAsync(string userName, string password);
        Task UpdateEmployee(EmployeeUpdateDto employeeModel);
    }
}