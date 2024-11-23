using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Database
{
    public class BudgetDBContext : IdentityDbContext<ApplicationUser>
    {
        public BudgetDBContext(DbContextOptions<BudgetDBContext> options) : base(options) { }

        public DbSet<Budget> Budgets {  get; set; }
        public DbSet<Category> Category {  get; set; }
        public DbSet<Transaction> Transactions {  get; set; }

    }

}
