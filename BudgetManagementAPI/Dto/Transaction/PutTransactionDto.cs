using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Transaction
{
    public class PutTransactionDto
    {
        [Required]
        public required long TransactionId { get; set; }

        [Required]
        public required long TransactionType { get; set; }

        [Required]
        public required decimal Amount { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly TransactionDate { get; set; }
    }
}
