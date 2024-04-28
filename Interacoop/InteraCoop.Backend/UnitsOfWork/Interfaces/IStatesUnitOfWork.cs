using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}
