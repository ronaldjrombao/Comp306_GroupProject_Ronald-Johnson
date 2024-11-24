using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Transaction
{
    public class PatchTransactionDto
    {
        public long? TransactionType { get; set; }

        public decimal Amount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly? TransactionDate { get; set; }
    }
}
