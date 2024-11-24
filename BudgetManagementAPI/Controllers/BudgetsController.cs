using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Identity;
using BudgetManagementAPI.Dto;

namespace BudgetManagementAPI.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository budgetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public BudgetsController(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            this.budgetRepository = budgetRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetItem>>> GetBudgets()
        {
            IEnumerable<BudgetItem> budgets = await this.budgetRepository.FindAllBudgetForUserAsync(this.userManager.GetUserId(HttpContext.User));
            return Ok(budgets);
        }

        //// GET: api/Budgets/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Budget>> GetBudget(int id)
        //{
        //    return Ok(null);
        //}

        //// PUT: api/Budgets/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBudget(int id, Budget budget)
        //{
        //    return NoContent();
        //}

        //// POST: api/Budgets
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Budget>> PostBudget(Budget budget)
        //{
        //    return CreatedAtAction("GetBudget", new { id = budget.BudgetId }, budget);
        //}

        //// DELETE: api/Budgets/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBudget(int id)
        //{
        //    return NoContent();
        //}

    }
}
