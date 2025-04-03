using AutoMapper;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Budget;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace BudgetManagementAPI.Tests
{
    public class BudgetRepositoryTests
    {
        private readonly Mock<BudgetDBContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BudgetRepository _budgetRepository;

        public BudgetRepositoryTests()
        {
            _dbContextMock = new Mock<BudgetDBContext>(new DbContextOptions<BudgetDBContext>());
            _mapperMock = new Mock<IMapper>();
            _budgetRepository = new BudgetRepository(_dbContextMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task FindAllBudgetForUserAsync_ShouldReturnBudgets_WhenBudgetsExist()
        {
            // Arrange
            var userId = "test-user-id";

            var budgets = new List<Budget>
            {
                new() {
                    BudgetId = 1,
                    Owner = new ApplicationUser { Id = userId },
                    BudgetType = new Category { CategoryName = "Test" },
                    StartDate = new DateOnly(2023, 1, 1),
                    EndDate = new DateOnly(2023, 12, 31),
                    BudgetName = "Test Budget",
                    Amount = 1000
                }
            };

            // Mock DbSet with Moq.EntityFrameworkCore
            _dbContextMock.Setup(db => db.Budgets).ReturnsDbSet(budgets);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Owner = new ApplicationUser { Id = userId },
                    TransactionType = new Category { CategoryName = "Test" },
                    TransactionDate = new DateOnly(2023, 6, 1),
                    Amount = 100
                }
            };

            _dbContextMock.Setup(db => db.Transactions).ReturnsDbSet(transactions);

            // Mock mapper to return a mapped BudgetItem from Budget
            _mapperMock.Setup(m => m.Map<Budget, BudgetItem>(It.IsAny<Budget>()))
                .Returns((Budget b) => new BudgetItem
                {
                    BudgetId = b.BudgetId,
                    BudgetName = b.BudgetName,
                    BudgetType = b.BudgetType?.CategoryName,
                    StartDate = b.StartDate.Value,
                    EndDate = b.EndDate.Value,
                    Amount = b.Amount,
                    CurrentAmount = 0 // Mocked value
                });

            // Act
            var result = await _budgetRepository.FindAllBudgetForUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);

            var budgetItem = result.First();
            Assert.Equal("Test Budget", budgetItem.BudgetName);
            Assert.Equal("Test", budgetItem.BudgetType);
            Assert.Equal(1000, budgetItem.Amount);
            Assert.Equal(0, budgetItem.CurrentAmount);
        }

        [Fact]
        public async Task FindBudgetByIdAndOwnerId_ShouldReturnBudget_WhenBudgetExists()
        {
            // Arrange
            var userId = "test-user-id";
            var budgetId = 1;
            var budget = new Budget { BudgetId = budgetId, Owner = new ApplicationUser { Id = userId }, BudgetName = "Test Budget", BudgetType = new Category { CategoryName = "Test" }, StartDate = new DateOnly(2023, 1, 1), EndDate = new DateOnly(2023, 12, 31), Amount = 1000 };

            _dbContextMock.Setup(db => db.Budgets).ReturnsDbSet(new List<Budget> { budget });

            // Act
            var result = await _budgetRepository.FindBudgetByIdAndOwnerId(budgetId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(budgetId, result.BudgetId);
        }
    }

    public static class DbSetMockExtensions
    {
        public static DbSet<T> ReturnsDbSet<T>(this Mock<DbSet<T>> dbSetMock, IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSetMock.Object;
        }

        public static void ReturnsDbSet<T>(this Mock<BudgetDBContext> dbContextMock, IEnumerable<T> data) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.ReturnsDbSet(data);
            dbContextMock.Setup(db => db.Set<T>()).Returns(dbSetMock.Object);
        }
    }
}
