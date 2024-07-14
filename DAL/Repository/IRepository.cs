using System.Linq.Expressions;

namespace RegistrationWizard.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] propertiesToJoin);
        Task<IList<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToJoin);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
