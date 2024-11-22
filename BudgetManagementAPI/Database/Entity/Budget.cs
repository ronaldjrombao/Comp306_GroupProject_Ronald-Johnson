using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Database.Entity
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
    
        public required string BudgetName { get; set; }

        public required Category BudgetType { get; set; }
    
        public required decimal Amount { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

        public required User Owner { get; set; }

    }
}
