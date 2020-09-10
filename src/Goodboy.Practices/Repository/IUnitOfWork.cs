using System;
using System.Threading;
using System.Threading.Tasks;

namespace Goodboy.Practices.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit operations.
        /// </summary>
        /// <returns>The </returns>
        Task<int> Commit(CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Detachs the entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void DetachEntity<T>(T entity) where T : class;
    }
}
