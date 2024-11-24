using System;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Transaction;

namespace BudgetManagementAPI.Repository;

public interface ITransactionRepository : IRepositoryBase<Transaction>
{
    Task<IEnumerable<TransactionItem>> FindAllTransactionForUserAsync(string userId);
    Task<Transaction?> FindTransactionByIdAndOwnerId(int transactionId, string userId);
}
