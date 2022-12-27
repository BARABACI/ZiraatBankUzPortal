using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeePhoneBookController : Controller
    {
        private readonly IEmployeePhoneBookRepository _employeePhoneBookRepository;

        public EmployeePhoneBookController(IEmployeePhoneBookRepository employeePhoneBookRepository)
        {
            _employeePhoneBookRepository = employeePhoneBookRepository;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<EmployeePhoneBookDisplayModel>>> Getall()
        {
            var phonebook = await _employeePhoneBookRepository.GetAllEmployeePhoneBookAsync();
            return Ok(phonebook);
        }
    }
}
