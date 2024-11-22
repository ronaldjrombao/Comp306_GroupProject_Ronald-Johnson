using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Database.Entity
{
    public class Transaction
    {
        [Key]
        public int BudgetId { get; set; }

        public required Category TransactionType { get; set; }

        public required decimal Amount { get; set; }

        public required DateTime TransactionDate { get; set; }
        
        public required User Owner { get; set; }
    }
}
