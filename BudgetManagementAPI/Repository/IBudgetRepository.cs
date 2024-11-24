using System;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto;

namespace BudgetManagementAPI.Repository;

public interface IBudgetRepository : IRepositoryBase<Budget>
{
    Task<IEnumerable<BudgetItem>> FindAllBudgetForUserAsync(string userId);
}
