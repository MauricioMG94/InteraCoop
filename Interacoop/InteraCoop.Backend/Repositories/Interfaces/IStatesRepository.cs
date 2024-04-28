using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }
}
