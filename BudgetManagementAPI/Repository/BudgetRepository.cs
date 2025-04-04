using AutoMapper;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;
using Microsoft.EntityFrameworkCore;
namespace BudgetManagementAPI.Repository;

public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
{

    private readonly IMapper mapper;
    public BudgetRepository(BudgetDBContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<BudgetItem>> FindAllBudgetForUserAsync(string userId, long? budgetId = null)
    {
        IQueryable<Budget> allBudget = _dbContext.Budgets;
        IList<BudgetItem> budgetItems = new List<BudgetItem>();
        IEnumerable<Budget> budgets;

        if (budgetId is not null)
        {
            budgets = await allBudget
                        .Include(b => b.BudgetType)
                        .Where(b => b.Owner.Id == userId)
                        .Where(b => b.BudgetId == budgetId).AsNoTracking().ToListAsync();
        }
        else
        {
            budgets = await allBudget
                        .Include(b => b.BudgetType)
                        .Where(b => b.Owner.Id == userId).AsNoTracking().ToListAsync();
        }

        foreach (var budget in budgets)
        {
            var currentAmount = await this._dbContext.Transactions
                .Where(t => t.Owner.Id == userId
                        && t.TransactionDate >= budget.StartDate
                        && t.TransactionDate <= budget.EndDate
                        && t.TransactionType == budget.BudgetType).AsNoTracking()
                .SumAsync(t => t.Amount);

            var budgetItemDTO = this.mapper.Map<Budget, BudgetItem>(budget);

            budgetItemDTO.CurrentAmount = currentAmount;
            
            budgetItems.Add(budgetItemDTO);
        }

        return budgetItems;
    }

    public async Task<Budget?> FindBudgetByIdAndOwnerId(long budgetId, string userId)
    {
        IQueryable<Budget> allBudget = this._dbContext.Budgets;
        
        Budget? existing = await allBudget
                                .Include(b => b.Owner)
                                .Where(b => b.Owner.Id == userId && b.BudgetId == budgetId)
                                .FirstOrDefaultAsync();

        return existing;
    }
}
