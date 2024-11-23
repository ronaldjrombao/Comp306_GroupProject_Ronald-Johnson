using System.Linq.Expressions;

namespace BudgetManagementAPI.Repository
{
    public interface IRepositoryBase<T, K> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(K id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
