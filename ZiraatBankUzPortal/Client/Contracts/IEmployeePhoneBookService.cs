namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface IEmployeePhoneBookService
    {
        Task<HttpResponseMessage> GetAllEmployeePhoneBook();
    }
}