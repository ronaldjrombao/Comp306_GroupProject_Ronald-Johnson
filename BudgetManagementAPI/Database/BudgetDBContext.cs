using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Database
{
    public class BudgetDBContext : IdentityDbContext<ApplicationUser>
    {
        public BudgetDBContext(DbContextOptions<BudgetDBContext> options) : base(options) {
        
        }

        public virtual DbSet<Budget> Budgets {  get; set; }
        public virtual DbSet<Category> Category {  get; set; }
        public virtual DbSet<Transaction> Transactions {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Budget>().Navigation(e => e.BudgetType).AutoInclude();
            builder.Entity<Transaction>().Navigation(e => e.TransactionType).AutoInclude();
        }
    }

}
