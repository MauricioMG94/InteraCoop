using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : GenericController<Campaign>
    {
        private readonly ICampaignsUnitOfWork _campaignsUnitOfWork;

        public CampaignsController(IGenericUnitOfWork<Campaign> unitOfWork, ICampaignsUnitOfWork campaignsUnitOfWork) : base(unitOfWork)
        {
            _campaignsUnitOfWork = campaignsUnitOfWork;
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _campaignsUnitOfWork.DeleteAsync(id);
            if (!action.WasSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _campaignsUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _campaignsUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _campaignsUnitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostAsync(CampaignDto campaignDto)
        {
            var action = await _campaignsUnitOfWork.AddAsync(campaignDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutAsync(CampaignDto campaignDto)
        {
            var action = await _campaignsUnitOfWork.UpdateAsync(campaignDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }
    }
}
