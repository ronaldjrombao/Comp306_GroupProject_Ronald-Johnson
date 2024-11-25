using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using BudgetManagementAPI.Dto.Transaction;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using BudgetManagementAPI.Dto;
using BudgetManagementAPI.Database.Entity;

namespace TransactionManagementAPI.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public TransactionController(ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<ApiResult<IEnumerable<TransactionItem>>>> GetBudgets()
        {
            ApiResult<IEnumerable<TransactionItem>> result = new();
            IEnumerable<TransactionItem> transactions = await this.transactionRepository.FindAllTransactionForUserAsync(this.userManager.GetUserId(HttpContext.User)!);
            result.Results = transactions;
            return Ok(result);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetBudget(int id)
        {
            ApiResult<TransactionItem> result = new();
            ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
            Transaction? transaction = await this.transactionRepository.FindTransactionByIdAndOwnerId(id, user!.Id);

            if (transaction != null && transaction.Owner.Id == this.userManager.GetUserId(HttpContext.User))
            {
                TransactionItem transactionItem = this.mapper.Map<TransactionItem>(transaction);
                result.Results = transactionItem;
                return Ok(result);
            }
            else
            {
                result.Message = "Transaction Not Found";
                return NotFound(result);
            }
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, [FromBody] PutTransactionDto transaction)
        {
            ApiResult<TransactionItem> apiResult = new();

            try
            {
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
                Transaction? existing = await this.transactionRepository.FindTransactionByIdAndOwnerId(id, user!.Id);

                if (existing != null && existing.TransactionId == id)
                {
                    existing = this.mapper.Map<PutTransactionDto, Transaction>(transaction, existing);
                    Category newCategory = await this.categoryRepository.FindById(transaction.TransactionType)!;
                    existing.TransactionType = newCategory;

                    await this.transactionRepository.SaveChangesAsync();
                    apiResult.Message = "Transaction Successfully updated";
                }
                else
                {
                    throw new Exception("Transaction was not found or does not belong to the user.");
                }

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostBudget([FromBody] CreateTransactionDto transaction)
        {
            ApiResult<TransactionItem> apiResult = new();

            try
            {
                Category category = await this.categoryRepository.FindById(transaction.TransactionType)!;
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);

                Transaction newTransaction = new Transaction()
                {
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = category,
                    Owner = user!
                };

                newTransaction = await this.transactionRepository.CreatAsync(newTransaction);
                apiResult.Results = this.mapper.Map<TransactionItem>(newTransaction);
                apiResult.Message = "Transaction Successfully Created";

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
        }

        // PATCH: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTransaction(int id, [FromBody] PatchTransactionDto transaction)
        {
            ApiResult<TransactionItem> apiResult = new();

            try
            {
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
                Transaction? existing = await this.transactionRepository.FindTransactionByIdAndOwnerId(id, user!.Id);

                if (existing != null && existing.TransactionId == id)
                {
                    existing = this.mapper.Map(transaction, existing);

                    if (transaction.TransactionType != null)
                    {
                        Category newCategory = await this.categoryRepository.FindById(transaction.TransactionType)!;
                        existing.TransactionType= newCategory;
                    }

                    await this.transactionRepository.SaveChangesAsync();
                    apiResult.Message = "Transaction Successfully updated";
                }
                else
                {
                    throw new Exception("Transaction was not found or does not belong to the user.");
                }

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
        }

        //// DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            await this.transactionRepository.DeleteByIdByAsync(id);
            return NoContent();
        }

    }
}
