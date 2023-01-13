using ZiraatBankUzPortal.Shared.Dto;

namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IEmployeeService
    {
        Task<HttpResponseMessage> CreateEmployee(EmployeeCreateDto createEmployee);
        Task<HttpResponseMessage> DeleteEmployee(int employeeId);
        Task<HttpResponseMessage> GetAllEmployee();
        Task<int> GetAuthId();
        Task<string> GetAuthName();
        Task<string> GetAuthRole();
        Task<HttpResponseMessage> GetEmployeeById(int employeeId);
        Task<HttpResponseMessage> GetEmployeeLocationComboBoxData();
        Task<HttpResponseMessage> GetEmployeePositionComboBoxData();
        Task<HttpResponseMessage> GetEmployeeTitleComboBoxData();
        Task<HttpResponseMessage> GetEmployeeDepartmentComboBoxData();
        Task<HttpResponseMessage> UpdateEmployee(EmployeeUpdateDto updateEmployee);
    }
}