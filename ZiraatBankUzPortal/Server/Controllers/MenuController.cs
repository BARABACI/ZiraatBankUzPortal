using Microsoft.AspNetCore.Mvc;
using ZiraatBankUzPortal.Server.Repositories;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenuDataRepository _menuDataRepository;

        public MenuController(IMenuDataRepository menuDataRepository)
        {
            _menuDataRepository = menuDataRepository;
        }

        [HttpGet("Getallmenu")]
        public async Task<ActionResult<IEnumerable<MenuModel>>> GetAllMenuAsync()
        {
            var menu = await _menuDataRepository.GetAllMenuAsync();
            return Ok(menu);
        }

        [HttpPost("CreateMenu")]
        public async Task<ActionResult> CreateUser(MenuModel menuModel)
        {
            await _menuDataRepository.CreateMenuAsync(menuModel);
            return Ok("Menu created.");
        }

        [HttpGet("Getmenubypagelink/{pageLink}")]
        public async Task<ActionResult<MenuModel>> GetMenuByPageLink(string pageLink)
        {
            var menu = await _menuDataRepository.GetMenuByPageLink(pageLink);
            return Ok(menu);
        }
    }
}
