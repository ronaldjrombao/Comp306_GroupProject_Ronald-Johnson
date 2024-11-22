using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Database.Entity
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category
    {
        [Key]
        public long CategoryId {  get; set; }

        public required string CategoryName { get; set; }

        public string? Description { get; set; }
    }
}
