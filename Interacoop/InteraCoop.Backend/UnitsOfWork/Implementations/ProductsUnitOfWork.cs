using InteraCoop.Backend.Repositories.Implementations;
using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Responses;

namespace InteraCoop.Backend.UnitsOfWork.Implementations
{
    public class ProductsUnitOfWork : GenericUnitOfWork<Product>, IProductsUnitOfWork
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsUnitOfWork(IGenericRepository<Product> repository, IProductsRepository productRepository) : base(repository)
        {
            _productsRepository = productRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination) => await _productsRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _productsRepository.GetTotalPagesAsync(pagination);

        public async Task<ActionResponse<Product>> AddAsync(ProductDto productDto) => await _productsRepository.AddAsync(productDto);

        public async Task<ActionResponse<Product>> UpdateAsync(ProductDto productDto) => await _productsRepository.UpdateAsync(productDto);

        public override async Task<ActionResponse<Product>> DeleteAsync(int id) => await _productsRepository.DeleteAsync(id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await _productsRepository.GetAllAsync();
    }
}
