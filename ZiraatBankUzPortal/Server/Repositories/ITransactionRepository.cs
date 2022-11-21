namespace ZiraatBankUzPortal.Server.Repositories
{
    public interface ITransactionRepository
    {
        Task DeleteTransaction(int transactionNo);
    }
}