using Dapper.Oracle;
using System.Data;
using ZiraatBankUzPortal.Core.Services;
using ZiraatBankUzPortal.Shared.DataAccess;
using ZiraatBankUzPortal.Shared.DisplayModel;
using ZiraatBankUzPortal.Shared.Dto;
using ZiraatBankUzPortal.Shared.Helper;
using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public class MenuDataRepository : IMenuDataRepository
    {
        private readonly IMenuDataService _menuDataService;
        public MenuDataRepository(IMenuDataService menuDataService)
        {
            _menuDataService = menuDataService;
        }
        public async Task<IEnumerable<MenuModel>> GetAllMenuAsync()
        {
            var menu = await _menuDataService.GetAllMenuAsync();
            return menu;
        }
        public async Task CreateMenuAsync(MenuModel menu)
        {
            await _menuDataService.CreateMenuAsync(menu);
        }

        public async Task<MenuModel> GetMenuByPageLink(string pageLink)
        {
            var menu = await _menuDataService.GetMenuByPageLink(pageLink);
            return menu;
        }
    }
}
