namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IPageRoleService
    {
        Task<HttpResponseMessage> GetPageRoles(string url);
    }
}