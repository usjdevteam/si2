using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _db;

        public Repository(DbContext db)
        {
            _db = db;
        }
        public IQueryable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken ct)
        {

            return await _db.Set<T>().ToListAsync(ct);
        }

        public virtual T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id, CancellationToken ct)
        {
            return await _db.Set<T>().FindAsync(new object[] { id }, ct);
        }

        public virtual T Add(T t)
        {

            _db.Set<T>().Add(t);
            _db.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsync(T t, CancellationToken ct)
        {
            _db.Set<T>().Add(t);
            await _db.SaveChangesAsync(ct);
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match, CancellationToken ct)
        {
            return await _db.Set<T>().SingleOrDefaultAsync(match, ct);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken ct)
        {
            return await _db.Set<T>().Where(match).ToListAsync(ct);
        }

        public virtual void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(T entity, CancellationToken ct)
        {
            _db.Set<T>().Remove(entity);
            return await _db.SaveChangesAsync(ct);
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _db.Set<T>().Find(key);
            if (exist != null)
            {
                _db.Entry(exist).CurrentValues.SetValues(t);
                _db.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsync(T t, object key, CancellationToken ct, byte[] rowVersion = null)
        {
            if (t == null)
                return null;
            T exist = await _db.Set<T>().FindAsync(key);
            if (exist != null)
            {
                if (rowVersion != null)
                    _db.Entry(exist).OriginalValues["RowVersion"] = rowVersion;
                _db.Entry(exist).CurrentValues.SetValues(t);

                await _db.SaveChangesAsync(ct);
            }
            return exist;
        }

        public int Count()
        {
            return _db.Set<T>().Count();
        }

        public async Task<int> CountAsync(CancellationToken ct)
        {
            return await _db.Set<T>().CountAsync(ct);
        }

        public virtual void Save()
        {

            _db.SaveChanges();
        }

        public async virtual Task<int> SaveAsync(CancellationToken ct)
        {
            return await _db.SaveChangesAsync(ct);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _db.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return await _db.Set<T>().Where(predicate).ToListAsync(ct);
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
