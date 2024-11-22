using BudgetManagementAPI.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Database
{
    public class BudgetDBContext : DbContext
    {
        public BudgetDBContext(DbContextOptions<BudgetDBContext> options) : base(options) { }

        public DbSet<Budget> Budgets {  get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Category {  get; set; }
        public DbSet<Transaction> Transactions {  get; set; }

    }

}
