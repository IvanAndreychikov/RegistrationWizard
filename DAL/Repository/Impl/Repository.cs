using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL.Context;
using System.Linq.Expressions;

namespace RegistrationWizard.DAL.Repository.Impl
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private bool _disposed;
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] propertiesToJoin)
        {
            var query = PrepareQueryWithJoins(propertiesToJoin);
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] propertiesToJoin)
        {
            var query = PrepareQueryWithJoins(propertiesToJoin);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<IList<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertiesToJoin)
        {
            var query = PrepareQueryWithJoins(propertiesToJoin);
            var filteredResult = query.Where(predicate);

            return await filteredResult.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private IQueryable<T> PrepareQueryWithJoins(params Expression<Func<T, object>>[] propertiesToJoin)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var propertyToJoin in propertiesToJoin)
            {
                query = query.Include(propertyToJoin);
            }
            return query;
        }
    }
}
