using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goodboy.Practices.Repository;

namespace Goodboy.Practices.Manager
{
    public class Manager<TEntity> : IManager<TEntity> where TEntity : class
    {
        readonly IRepository<TEntity> _repository;
        readonly IUnitOfWork _unitOfWork;

        public Manager(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> Create(TEntity entity, CancellationToken cancelationToken = default(CancellationToken))
        {
            _repository.Add(entity, cancelationToken);
            return await _unitOfWork.Commit(cancelationToken) > 0 ? entity : null;
        }

        public virtual async Task<TEntity> Delete(Guid id, CancellationToken cancelationToken = default(CancellationToken))
        {
            var entity = await _repository.GetAsync(id, cancelationToken);

            if (entity == null)
            {
                return null;
            }

            _repository.Delete(entity, cancelationToken);
            return await _unitOfWork.Commit(cancelationToken) > 0 ? entity : null;
        }

        public virtual Task<TEntity> Get(Guid id, CancellationToken cancelationToken = default(CancellationToken))
        {
            return _repository.GetAsync(id, cancelationToken);
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetList(int? index, int? offset, CancellationToken cancelationToken = default(CancellationToken))
        {
            return new Tuple<IEnumerable<TEntity>, int>(
                await _repository.GetAllAsync(index, offset, cancelationToken),
                await _repository.Count(cancelationToken));
        }

        public virtual async Task<TEntity> Update(TEntity entity, CancellationToken cancelationToken = default(CancellationToken))
        {
            _repository.Update(entity, cancelationToken);
            return await _unitOfWork.Commit(cancelationToken) >= 0 ? entity : null;
        }
    }
}
