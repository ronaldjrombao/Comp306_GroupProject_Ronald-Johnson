using AutoMapper;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Transaction;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Repository;

public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
{

    private readonly IMapper mapper;
    public TransactionRepository(BudgetDBContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TransactionItem>> FindAllTransactionForUserAsync(string userId)
    {
        IQueryable<Transaction> allTransaction = this.FindAll();
        IEnumerable<TransactionItem> transactions = await allTransaction
                                                        .Include(b => b.TransactionType)
                                                        .Where(b => b.Owner.Id == userId)
                                                        .Select(budget => this.mapper.Map<TransactionItem>(budget)).AsNoTracking().ToListAsync();
        return transactions;
    }

    public async Task<Transaction?> FindTransactionByIdAndOwnerId(long transactionId, string userId)
    {
        IQueryable<Transaction> allTransaction = this.FindAll();
        Transaction? existing = await allTransaction
                                    .Include(b => b.Owner)
                                    .Where(b => b.Owner.Id == userId && transactionId == b.TransactionId)
                                    .FirstOrDefaultAsync();

        return existing;
    }
}
