using System.Linq.Expressions;

namespace BudgetManagementAPI.Repository
{
    public interface IRepositoryBase<T> where T : class 
    {
        Task CreatAsync(T entity);
        Task UpdateAsync(T entity);
        IQueryable<T> FindAll();
        Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> filter);
        Task<T>? FindById(object id);
        Task DeleteByIdByAsync(object id);
    }
}
