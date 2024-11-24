using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBudgetRepository budgetRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                        IBudgetRepository budgetRepository,
                                        ICategoryRepository categoryRepository,
                                        UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.budgetRepository = budgetRepository;
            this.categoryRepository = categoryRepository;
            this._userManager = userManager;
        }

        [HttpGet(Name = "GetWeatherForecast"), Authorize]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            IEnumerable<Category> categories = await this.categoryRepository.FindAll().ToListAsync();
            Category? category = categories.FirstOrDefault<Category>();
            ApplicationUser? user = await this._userManager.GetUserAsync(HttpContext.User);
            Budget newBudget = new()
            {
                BudgetName = "No Spend Food",
                Amount = 100.00m,
                BudgetType = category,
                StartDate = DateOnly.Parse(DateTime.Now.ToString()),
                EndDate = DateOnly.Parse(DateTime.Now.ToString()),
                Owner = user
            };

            await this.budgetRepository.CreatAsync(newBudget);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
