namespace ZiraatBankUzPortal.Client.Contracts
{
    public interface ITransactionService
    {
        Task<HttpResponseMessage> DeleteTransaction(int transactionId);
    }
}