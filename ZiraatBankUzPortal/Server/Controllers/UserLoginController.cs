using Microsoft.AspNetCore.Mvc;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared.DisplayModel;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserDataRepository _userDataRepository;

        public UserLoginController(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        [HttpGet("{userName}/{userPassword}")]
        public async Task<ActionResult<DisplayLoginUserModel>> GetUserLoginStatus(string userName, string userPassword)
        {
            var user = await _userDataRepository.GetLoginUserByIdAsync(userName,userPassword);
            return Ok(user);
        }
    }
}
