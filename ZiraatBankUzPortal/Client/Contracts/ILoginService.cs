namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface ILoginService
    {
        Task<HttpResponseMessage> Login(string userName, string password);
    }
}