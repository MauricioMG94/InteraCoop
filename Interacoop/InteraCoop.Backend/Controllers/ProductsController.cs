using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : GenericController<Product>
    {
        private readonly IProductsUnitOfWork _productsUnitOfWork;

        public ProductsController(IGenericUnitOfWork<Product> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
