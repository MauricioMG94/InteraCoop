using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.Repositories.Interfaces
{
    public interface ICountriesRepository
    {
      Task<ActionResponse<Country>> GetAsync(int id);  
      Task<ActionResponse<IEnumerable<Country>>> GetAsync();  
    }
}
