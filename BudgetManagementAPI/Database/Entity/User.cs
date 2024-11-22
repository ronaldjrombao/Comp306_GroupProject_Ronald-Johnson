using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Database.Entity
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public long UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

    }
}
