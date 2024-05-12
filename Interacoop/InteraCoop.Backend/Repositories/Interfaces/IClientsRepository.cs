using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface IClientsRepository
    {
        Task<ActionResponse<Client>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Client>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Client>> AddFullAsync(ClientDto clientDto);

        Task<ActionResponse<Client>> UpdateFullAsync(ClientDto clientDto);
    }
}
