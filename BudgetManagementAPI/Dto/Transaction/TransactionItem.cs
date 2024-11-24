using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Transaction
{
    public class TransactionItem
    {
        public long TransactionId { get; set; }

        public required string TransactionType { get; set; }

        public required decimal Amount { get; set; }

        public required DateOnly TransactionDate { get; set; }

    }
}
