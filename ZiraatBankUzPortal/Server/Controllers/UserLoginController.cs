using Microsoft.AspNetCore.Mvc;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IEmployeeDataRepository _userDataRepository;

        public UserLoginController(IEmployeeDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        [HttpGet("{userName}/{userPassword}")]
        public async Task<ActionResult<LoginUserDisplayModel>> GetUserLoginStatus(string userName, string userPassword)
        {
            var user = await _userDataRepository.GetLoginEmployeeByIdAsync(userName,userPassword);
            return Ok(user);
        }
    }
}
