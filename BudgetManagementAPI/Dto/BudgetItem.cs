using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto
{
    public class BudgetItem
    {
        public int BudgetId { get; set; }

        public required string BudgetName { get; set; }

        public required string BudgetType { get; set; }

        public required decimal Amount { get; set; }

        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

    }
}
