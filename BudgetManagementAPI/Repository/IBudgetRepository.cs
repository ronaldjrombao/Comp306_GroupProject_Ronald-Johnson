using System;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;

namespace BudgetManagementAPI.Repository;

public interface IBudgetRepository : IRepositoryBase<Budget>
{
    Task<IEnumerable<BudgetItem>> FindAllBudgetForUserAsync(string userId, long? budgetId = null);
    Task<Budget?> FindBudgetByIdAndOwnerId(long budgetId, string userId);
}
