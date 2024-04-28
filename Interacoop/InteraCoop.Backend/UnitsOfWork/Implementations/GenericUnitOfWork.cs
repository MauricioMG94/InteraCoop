using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repostory;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repostory = repository;
        }
        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repostory.GetAsync(pagination);
        public virtual async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _repostory.GetTotalPagesAsync(pagination);
        public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repostory.AddAsync(model);
        public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repostory.DeleteAsync(id);
        public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repostory.GetAsync(id);
        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repostory.GetAsync();
        public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repostory.UpdateAsync(model);
    }
}
