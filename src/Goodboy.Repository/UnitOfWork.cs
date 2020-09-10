using System;
using System.Threading;
using System.Threading.Tasks;
using Goodboy.Practices.Repository;
using Microsoft.EntityFrameworkCore;

namespace Goodboy.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public Task<int> Commit(CancellationToken cancelationToken = default(CancellationToken))
        {
            return _context.SaveChangesAsync(cancelationToken);
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached;

            var s = _context.Entry(entity).State;
        }

        #region Dispose
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    _context.Dispose();
                    _disposed = true;
                }
            }
        }
        #endregion
    }
}
