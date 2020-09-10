using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goodboy.Practices.Manager
{
    public interface IManager<TEntity> where TEntity : class 
    {
        /// <summary>
        /// Gets a list of Entities
        /// </summary>
        /// <returns>The entities list.</returns>
        /// <param name="index">Index.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        Task<Tuple<IEnumerable<TEntity>, int>> GetList(int? index, int? offset, CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Gets an Entity
        /// </summary>
        /// <returns>The Entity.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        Task<TEntity> Get(Guid id, CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Creates an Entity
        /// </summary>
        /// <returns>The created Entity.</returns>
        /// <param name="entity">Entity to be created.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        Task<TEntity> Create(TEntity entity, CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Updates the Entity
        /// </summary>
        /// <returns>The updated entity.</returns>
        /// <param name="entity">Entity to update.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        Task<TEntity> Update(TEntity entity, CancellationToken cancelationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the Entity.
        /// </summary>
        /// <returns>The deleted Entity.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        Task<TEntity> Delete(Guid id, CancellationToken cancelationToken = default(CancellationToken));
    }
}
