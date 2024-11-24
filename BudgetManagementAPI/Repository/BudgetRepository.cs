using System;
using System.Linq.Expressions;
using AutoMapper;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Repository;

public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
{

    private readonly IMapper mapper;
    public BudgetRepository(BudgetDBContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<BudgetItem>> FindAllBudgetForUserAsync(string userId)
    {
        IQueryable<Budget> allBudget = this.FindAll();
        IEnumerable<BudgetItem> budgets = await allBudget
                                                        .Include(b => b.BudgetType)
                                                        .Where(b => b.Owner.Id == userId)
                                                        .Select(budget => this.mapper.Map<BudgetItem>(budget)).AsNoTracking().ToListAsync();
        return budgets;
    }

    public async Task<Budget?> FindBudgetByIdAndOwnerId(long budgetId, string userId)
    {
        IQueryable<Budget> allBudget = this.FindAll();
        Budget? existing = await allBudget
                                .Include(b => b.Owner)
                                .Where(b => b.Owner.Id == userId && b.BudgetId == budgetId)
                                .FirstOrDefaultAsync();

        return existing;
    }
}
