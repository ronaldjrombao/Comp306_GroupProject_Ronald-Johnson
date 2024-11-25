using Microsoft.AspNetCore.Mvc;
using BudgetManagementAPI.Database.Entity;
using Microsoft.AspNetCore.Authorization;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using BudgetManagementAPI.Dto.Budget;
using BudgetManagementAPI.Dto;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository budgetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        public BudgetsController(IBudgetRepository budgetRepository, 
            ICategoryRepository categoryRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.budgetRepository = budgetRepository;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<ApiResult<IEnumerable<BudgetItem>>>> GetBudgets()
        {
            ApiResult<IEnumerable<BudgetItem>> result = new();
            IEnumerable<BudgetItem> budgets = await this.budgetRepository.FindAllBudgetForUserAsync(this.userManager.GetUserId(HttpContext.User)!);
            result.Results = budgets;
            return Ok(result);
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudget(long id)
        {
            ApiResult<BudgetItem> result = new();
            ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
            Budget? budget = await this.budgetRepository.FindBudgetByIdAndOwnerId(id, user!.Id);
            
            if (budget != null && budget.Owner.Id == this.userManager.GetUserId(HttpContext.User))
            {
                BudgetItem budgetItem = this.mapper.Map<BudgetItem>(budget);
                result.Results = budgetItem;
                return Ok(result);
            }
            else
            {
                result.Message = "Budget Not Found";
                return NotFound(result);   
            }
        }

        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(long id, [FromBody] PutBudgetDto budget)
        {
            ApiResult<BudgetItem> apiResult = new();

            try
            {
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
                Budget? existing = await this.budgetRepository.FindBudgetByIdAndOwnerId(id, user!.Id);

                if (existing != null && existing.BudgetId == id)
                {
                    existing = this.mapper.Map<PutBudgetDto, Budget>(budget, existing);

                    Category newCategory = await this.categoryRepository.FindById(budget.BudgetType)!;

                    existing.BudgetType = newCategory;

                    await this.budgetRepository.SaveChangesAsync();
                    apiResult.Message = "Budget Successfully updated";
                } else
                {
                    throw new Exception("Budget was not found or does not belong to the user.");
                }

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
        }

        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Budget>> PostBudget([FromBody] CreateBudgetDto budget)
        {
            ApiResult<BudgetItem> apiResult = new();
            
            try
            {
                Category category = await this.categoryRepository.FindById(budget.BudgetType)!;
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);

                Budget newBudget = new Budget() 
                { 
                    BudgetName = budget.BudgetName,
                    Amount = budget.Amount,
                    StartDate = budget.StartDate,
                    EndDate = budget.EndDate,
                    BudgetType = category,
                    Owner = user!
                };

                newBudget = await this.budgetRepository.CreatAsync(newBudget);
                apiResult.Results = this.mapper.Map<BudgetItem>(newBudget);
                apiResult.Message = "Budget Successfully Created";

                return Ok(apiResult);

            } catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
    }

        // PATCH: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBudget(int id, [FromBody] PatchBudgetDto budget)
        {
            ApiResult<BudgetItem> apiResult = new();

            try
            {
                ApplicationUser? user = await this.userManager.GetUserAsync(HttpContext.User);
                Budget? existing = await this.budgetRepository.FindBudgetByIdAndOwnerId(id, user!.Id);

                if (existing != null && existing.BudgetId == id)
                {

                    if (budget.BudgetType != null)
                    {
                        Category newCategory = await this.categoryRepository.FindById(budget.BudgetType)!;
                        existing.BudgetType = newCategory;
                    }

                    mapper.Map<PatchBudgetDto, Budget>(budget, existing);

                    await this.budgetRepository.SaveChangesAsync();
                    apiResult.Message = "Budget Successfully updated";
                } else
                {
                    throw new Exception("Budget was not found or does not belong to the user.");
                }

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                apiResult.Message = ex.Message;
                return BadRequest(apiResult);
            }
        }

        //// DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(long id)
        {
            await this.budgetRepository.DeleteByIdByAsync(id);
            return NoContent();
        }

    }
}
