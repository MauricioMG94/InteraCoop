using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;
using InteraCoop.Shared.Dtos;

namespace InteraCoop.Backend.UnitsOfWork.Interfaces
{
    public interface IProductsUnitOfWork
    {

        Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
        Task<ActionResponse<Product>> DeleteAsync(int id);
        Task<ActionResponse<Product>> UpdateAsync(ProductDto product);
        Task<ActionResponse<Product>> AddAsync(ProductDto productDTO);

    }
}
