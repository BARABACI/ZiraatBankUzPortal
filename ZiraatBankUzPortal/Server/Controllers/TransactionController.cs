using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZiraatBankUzPortal.Server.Repositories;

namespace ZiraatBankUzPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpDelete("DeleteTransaction/{transactionId}")]
        public async Task<ActionResult> DeleteUser(int transactionId)
        {
            await _transactionRepository.DeleteTransaction(transactionId);
            return Ok("Transaction deleted.");
        }
    }
}
