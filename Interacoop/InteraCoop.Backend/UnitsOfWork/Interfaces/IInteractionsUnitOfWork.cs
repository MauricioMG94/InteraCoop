using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface IInteractionsUnitOfWork
    {
        Task<ActionResponse<Interaction>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Interaction>>> GetAsync();
        Task<ActionResponse<IEnumerable<Interaction>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<Interaction>> AddAsync(InteractionDto interaction);
        Task<ActionResponse<Interaction>> UpdateAsync(InteractionDto interaction);
        Task<ActionResponse<Interaction>> DeleteAsync(int id);

    }
}
