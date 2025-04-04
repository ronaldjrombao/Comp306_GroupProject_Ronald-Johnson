﻿using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Database.Entity
{
    public class Budget
    {
        public Budget() { }
        [Key]
        public long BudgetId { get; set; }
    
        public required string BudgetName { get; set; }

        public required Category BudgetType { get; set; }

        [Precision(18, 2)]
        public required decimal Amount { get; set; }

        public required DateOnly? StartDate { get; set; }

        public required DateOnly? EndDate { get; set; }

        public required ApplicationUser Owner { get; set; }

    }
}
