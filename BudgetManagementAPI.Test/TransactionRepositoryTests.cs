using AutoMapper;
using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using BudgetManagementAPI.Dto.Transaction;
using BudgetManagementAPI.Repository;
using BudgetManagementAPI.Security;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace BudgetManagementAPI.Tests
{
    public class TransactionRepositoryTests
    {
        private readonly Mock<BudgetDBContext> _dbContextMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TransactionRepository _transactionRepository;

        public TransactionRepositoryTests()
        {
            _dbContextMock = new Mock<BudgetDBContext>(new DbContextOptions<BudgetDBContext>());
            _mapperMock = new Mock<IMapper>();
            _transactionRepository = new TransactionRepository(_dbContextMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task FindAllTransactionForUserAsync_ShouldReturnTransactions_WhenTransactionsExist()
        {
            // Arrange
            var userId = "test-user-id";

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = 1,
                    Owner = new ApplicationUser { Id = userId },
                    TransactionType = new Category { CategoryName = "Groceries" },
                    TransactionDate = new DateOnly(2024, 1, 1),
                    Amount = 200
                }
            };

            _dbContextMock.Setup(db => db.Transactions).ReturnsDbSet(transactions);

            _mapperMock.Setup(m => m.Map<TransactionItem>(It.IsAny<Transaction>()))
                .Returns((Transaction t) => new TransactionItem
                {
                    TransactionId = t.TransactionId,
                    TransactionType = t.TransactionType?.CategoryName,
                    TransactionDate = t.TransactionDate.Value,
                    Amount = t.Amount
                });

            // Act
            var result = await _transactionRepository.FindAllTransactionForUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);

            var transaction = result.First();
            Assert.Equal(1, transaction.TransactionId);
            Assert.Equal("Groceries", transaction.TransactionType);
            Assert.Equal(200, transaction.Amount);
        }

        [Fact]
        public async Task FindTransactionByIdAndOwnerId_ShouldReturnTransaction_WhenMatchExists()
        {
            // Arrange
            var userId = "test-user-id";
            var transactionId = 1L;

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = transactionId,
                    Owner = new ApplicationUser { Id = userId },
                    TransactionType = new Category { CategoryName = "Groceries" },
                    TransactionDate = new DateOnly(2024, 1, 1),
                    Amount = 200
                }
            };

            _dbContextMock.Setup(db => db.Transactions).ReturnsDbSet(transactions);

            // Act
            var result = await _transactionRepository.FindTransactionByIdAndOwnerId(transactionId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transactionId, result.TransactionId);
            Assert.Equal(userId, result.Owner.Id);
        }

        [Fact]
        public async Task FindTransactionByIdAndOwnerId_ShouldReturnNull_WhenNoMatch()
        {
            // Arrange
            var userId = "test-user-id";
            var wrongUserId = "other-user";
            var transactionId = 1L;

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = transactionId,
                    Owner = new ApplicationUser { Id = wrongUserId }, // mismatched user
                    TransactionType = new Category { CategoryName = "Groceries" },
                    TransactionDate = new DateOnly(2024, 1, 1),
                    Amount = 200
                }
            };

            _dbContextMock.Setup(db => db.Transactions).ReturnsDbSet(transactions);

            // Act
            var result = await _transactionRepository.FindTransactionByIdAndOwnerId(transactionId, userId);

            // Assert
            Assert.Null(result);
        }
    }
}

