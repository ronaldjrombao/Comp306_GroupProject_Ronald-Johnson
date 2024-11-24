using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Budget
{
    public class PutBudgetDto
    {
        [Required]
        public required long BudgetId { get; set; }

        [Required]
        public required string BudgetName { get; set; }

        [Required]
        public required long BudgetType { get; set; }

        [Required]
        public required decimal Amount { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly StartDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly EndDate { get; set; }
    }
}
