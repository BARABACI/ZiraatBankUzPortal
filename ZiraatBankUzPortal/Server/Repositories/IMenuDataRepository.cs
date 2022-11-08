using ZiraatBankUzPortal.Shared.Model;

namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface IMenuDataRepository
    {
        Task<IEnumerable<MenuModel>> GetAllMenuAsync();
        Task CreateMenuAsync(MenuModel menu);
        Task<MenuModel> GetMenuByPageLink(string pageLink);
    }
}