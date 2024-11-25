using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto.Budget
{
    public class BudgetItem
    {
        public long BudgetId { get; set; }

        public required string BudgetName { get; set; }

        public required string BudgetType { get; set; }

        public required decimal Amount { get; set; }

        public required DateOnly StartDate { get; set; }

        public required DateOnly EndDate { get; set; }

        public required decimal CurrentAmount { get; set; }

    }
}
