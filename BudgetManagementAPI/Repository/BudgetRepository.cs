using System;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;

namespace BudgetManagementAPI.Repository;

public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
{
    public BudgetRepository(BudgetDBContext dbContext) : base(dbContext){}
}
