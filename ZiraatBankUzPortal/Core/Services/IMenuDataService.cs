using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Core.Services
{
    public interface IMenuDataService
    {
        Task<IEnumerable<MenuModel>> GetAllMenuAsync();
        Task CreateMenuAsync(MenuModel menu);
        Task<MenuModel> GetMenuByPageLink(string pageLink);
    }
}