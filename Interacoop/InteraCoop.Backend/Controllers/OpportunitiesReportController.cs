using InteraCoop.Backend.Repositories.Interfaces;
using InteraCoop.Backend.UnitsOfWork.Interfaces;
using InteraCoop.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace InteraCoop.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpportunitiesReportController : GenericController<ReportDto>
    {
        private readonly IOpportunitiesReportRepository _reportsRepository;

        public OpportunitiesReportController(IGenericUnitOfWork<ReportDto> unitOfWork, IOpportunitiesReportRepository reportsRepository) : base(unitOfWork)
        {
            _reportsRepository = reportsRepository;
        }

        [HttpGet("opportunitiesReports")]
        public async Task<IActionResult> GetOpportunitiesReport([FromQuery] PaginationDTO pagination)
        {
            var action = await _reportsRepository.GetOpportunitiesReportAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action);
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _reportsRepository.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
