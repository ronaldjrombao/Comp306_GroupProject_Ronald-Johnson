using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Budget
{
    public class PatchBudgetDto
    {
        public string? BudgetName { get; set; }

        public long? BudgetType { get; set; }

        public  decimal? Amount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly? StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly? EndDate { get; set; }
    }
}
