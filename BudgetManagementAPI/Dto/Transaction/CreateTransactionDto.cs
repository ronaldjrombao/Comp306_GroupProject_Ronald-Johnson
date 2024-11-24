using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Transaction
{
    public class CreateTransactionDto
    {
        [Required]
        public required long TransactionType { get; set; }

        [Required]
        public required decimal Amount { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateOnly TransactionDate { get; set; }
    }
}
