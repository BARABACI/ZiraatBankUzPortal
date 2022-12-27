using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.Model;
using ZiraatBankUzPortal.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDataRepository _employeeDataRepository;

        public EmployeeController(IEmployeeDataRepository employeeDataRepository)
        {
            _employeeDataRepository = employeeDataRepository;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> Getall()
        {
            var user = await _employeeDataRepository.GetAllEmployeeAsync();
            return Ok(user);
        }

        [HttpGet("GetEmployeeById/{employeeId}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeById(int employeeId)
        {
            var user = await _employeeDataRepository.GetEmployeeByIdAsync(employeeId);
            return Ok(user);
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            await _employeeDataRepository.DeleteEmployee(employeeId);
            return Ok("employeeDtoModel deleted.");
        }

        [HttpPost("CreateEmployee")]
        public async Task<ActionResult> CreateUser(EmployeeCreateDto employeeCreateDtoModel)
        {
            await _employeeDataRepository.CreateEmployee(employeeCreateDtoModel);
            return Ok("employeeDtoModel created.");
        }


        [HttpPost("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeUpdateDto employeeUpdateDtoModel)
        {
            await _employeeDataRepository.UpdateEmployee(employeeUpdateDtoModel);
            return Ok("Employee updated.");
        }

        [HttpGet("GetEmployeeTitleComboBoxData")]
        public async Task<ActionResult<EmployeeTitleDto>> GetEmployeeTitleComboBoxData()
        {
            var data = await _employeeDataRepository.GetEmployeeTitleComboBoxData();
            return Ok(data);
        }

        [HttpGet("GetEmployeePositionComboBoxData")]
        public async Task<ActionResult<EmployeePositionDto>> GetEmployeePositionComboBoxData()
        {
            var data = await _employeeDataRepository.GetEmployeePositionComboBoxData();
            return Ok(data);
        }

        [HttpGet("GetEmployeeLocationComboBoxData")]
        public async Task<ActionResult<EmployeeLocationDto>> GetEmployeeLocationComboBoxData()
        {
            var data = await _employeeDataRepository.GetEmployeeLocationComboBoxData();
            return Ok(data);
        }

    }
}
