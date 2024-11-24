using System;
using System.Linq.Expressions;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Repository;

public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
{
    public BudgetRepository(BudgetDBContext dbContext) : base(dbContext){}

    public async Task<IEnumerable<BudgetItem>> FindAllBudgetForUserAsync(string userId)
    {
        IQueryable<Budget> allBudget = this.FindAll();
        IEnumerable<BudgetItem> budgets = await allBudget
                                                        .Include(b => b.BudgetType)
                                                        .Where(b => b.Owner.Id == userId)
                                                        .Select(budget => new BudgetItem()
                                                        {
                                                            BudgetId = budget.BudgetId,
                                                            BudgetName = budget.BudgetName,
                                                            Amount = budget.Amount,
                                                            StartDate = budget.StartDate,
                                                            EndDate = budget.EndDate,
                                                            BudgetType = budget.BudgetType.CategoryName
                                                        }).AsNoTracking().ToListAsync();
        return budgets;
    }

}
