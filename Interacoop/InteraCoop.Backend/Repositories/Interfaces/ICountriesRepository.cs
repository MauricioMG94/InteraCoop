using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);  
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();  
        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);  
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);  

        Task<IEnumerable<Country>> GetComboAsync();
    }
}
