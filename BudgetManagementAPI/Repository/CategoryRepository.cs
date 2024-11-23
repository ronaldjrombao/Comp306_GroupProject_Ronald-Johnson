using System;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;

namespace BudgetManagementAPI.Repository;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(BudgetDBContext dbContext) : base(dbContext)
    {
    }
}
