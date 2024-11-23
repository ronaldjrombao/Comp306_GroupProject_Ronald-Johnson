using System;
using System.Linq.Expressions;
using BudgetManagementAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T: class
{
    private readonly BudgetDBContext _dbContext;
    public DbSet<T> DbSet {get; private set;}

    public RepositoryBase(BudgetDBContext dbContext) {
        this._dbContext =  dbContext;
        this.DbSet = this._dbContext.Set<T>();
    }

    public async Task CreatAsync(T entity)
    {
        this.DbSet.Add(entity);
        await this.SaveChangesAsync();
    }

    public async Task DeleteByIdByAsync(object id)
    {
        var entityToDelete = await DbSet.FindAsync(id);

        if (entityToDelete != null)
        {
            DbSet.Remove(entityToDelete);
            await SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> filter)
    {
        return await this.DbSet.Where<T>(filter).AsNoTracking<T>().ToListAsync();
    }

    public async Task<T>? FindById(object id)
    {
        return await this.DbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        this.DbSet.Update(entity);
        await this.SaveChangesAsync();
    }

    private async Task SaveChangesAsync() {
        await this._dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAll()
    {
        return await this.DbSet.ToListAsync<T>();
    }
}
