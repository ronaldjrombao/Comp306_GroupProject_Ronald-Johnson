using BudgetManagementAPI.Database;
using BudgetManagementAPI.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace BudgetManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly BudgetDBContext context;
        private readonly DbSet<User> userEntity;

        public UserRepository(BudgetDBContext context)
        {
            this.context = context;
            this.userEntity = context.Users;
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllByCondition(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }


public static class UserEndpoints
{
	public static void MapUserEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        group.MapGet("/", async (BudgetDBContext db) =>
        {
            return await db.Users.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (long userid, BudgetDBContext db) =>
        {
            return await db.Users.AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserId == userid)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (long userid, User user, BudgetDBContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.UserId == userid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.UserId, user.UserId)
                  .SetProperty(m => m.Username, user.Username)
                  .SetProperty(m => m.Password, user.Password)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        group.MapPost("/", async (User user, BudgetDBContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.UserId}",user);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (long userid, BudgetDBContext db) =>
        {
            var affected = await db.Users
                .Where(model => model.UserId == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}}
