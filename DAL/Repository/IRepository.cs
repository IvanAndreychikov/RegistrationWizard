using System.Linq.Expressions;

namespace RegistrationWizard.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[] propertiesToJoin);
        Task<IList<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] propertiesToJoin);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
