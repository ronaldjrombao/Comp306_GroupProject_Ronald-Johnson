using System.ComponentModel.DataAnnotations;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Database.Entity
{
    public class Transaction
    {
        [Key]
        public int BudgetId { get; set; }

        public required Category TransactionType { get; set; }

        [Precision(18, 2)]
        public required decimal Amount { get; set; }

        public required DateTime TransactionDate { get; set; }
        
        public required ApplicationUser Owner { get; set; }
    }
}
