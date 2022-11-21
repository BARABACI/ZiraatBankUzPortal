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
    public class UserController : ControllerBase
    {
        private readonly IUserDataRepository _userDataRepository;

        public UserController(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Getall()
        {
            var user = await _userDataRepository.GetAllUserAsync();
            return Ok(user);
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<ActionResult<UserModel>> GetUserById(int userId)
        {
            var user = await _userDataRepository.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            await _userDataRepository.DeleteUser(userId);
            return Ok("User deleted.");
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(CreateUserDto userModel)
        {
            await _userDataRepository.CreateUser(userModel);
            return Ok("User created.");
        }


        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto userModel)
        {
            await _userDataRepository.UpdateUser(userModel);
            return Ok("User updated.");
        }

        [HttpGet("GetUserTitleComboBoxData")]
        public async Task<ActionResult<UserTitleDto>> GetUserTitleComboBoxData()
        {
            var data = await _userDataRepository.GetUserTitleComboBoxData();
            return Ok(data);
        }

        [HttpGet("GetUserPositionComboBoxData")]
        public async Task<ActionResult<UserPositionDto>> GetUserPositionComboBoxData()
        {
            var data = await _userDataRepository.GetUserPositionComboBoxData();
            return Ok(data);
        }

        [HttpGet("GetUserLocationComboBoxData")]
        public async Task<ActionResult<UserLocationDto>> GetUserLocationComboBoxData()
        {
            var data = await _userDataRepository.GetUserLocationComboBoxData();
            return Ok(data);
        }

    }
}
