using InteraCoop.Backend.UnitsOfWork.Implementations;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProductsController : GenericController<Product>
    {
        private readonly IProductsUnitOfWork _productsUnitOfWork;

        public ProductsController(IGenericUnitOfWork<Product> unitOfWork, IProductsUnitOfWork productsUnitOfWork) : base(unitOfWork)
        {
            _productsUnitOfWork = productsUnitOfWork;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _productsUnitOfWork.GetAllAsync());
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new Exception("User ID claim not found or invalid.");
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _productsUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var action = await _productsUnitOfWork.GetAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action);
        }


        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _productsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostAsync(ProductDto productDTO)
        {
            var action = await _productsUnitOfWork.AddAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutAsync(ProductDto productDTO)
        {
            var action = await _productsUnitOfWork.UpdateAsync(productDTO);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _productsUnitOfWork.DeleteAsync(id);
            if (!action.WasSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
