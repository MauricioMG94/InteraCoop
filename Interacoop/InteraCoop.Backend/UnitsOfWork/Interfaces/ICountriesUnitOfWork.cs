using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using System.Collections;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
    }
}